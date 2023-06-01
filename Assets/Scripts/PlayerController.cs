using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    const float DETECTION_RANGE = 20f;

    [Header("References")]
    [SerializeField] Transform _crystal;
    [SerializeField] PlayerReflection _playerReflection;

    [Header("Movement Parameters")]
    [SerializeField] float _movementSpeed;
    [SerializeField, Tooltip("Acceleration over time")] AnimationCurve _movementCurve;

    [Header("Rotation Parameters")]
    [SerializeField] float _rotationSpeed;
    [SerializeField] bool _eightLaserDirections;
    [SerializeField, Tooltip("Movement curve of the crystal")] AnimationCurve _rotationCurve;

    [Header("Events")]
    [SerializeField] UnityEvent _onPlayerMoveStarted;
    [SerializeField] UnityEvent _onPlayerMoveStopped;
    [SerializeField] UnityEvent _onPlayerRotationStarted;
    [SerializeField] UnityEvent _onPlayerRotationStopped;
    [SerializeField] UnityEvent _onCrateMoveStart;
    [SerializeField] UnityEvent _onCrateMoveStop;

    Coroutine _movementCoroutine;
    Coroutine _rotationCoroutine;
    Coroutine _moveCrateCoroutine;
    RaycastHit2D _crateRay;

    const float ANGLE_TOLERANCE = 2f;
    public bool IsMoving
    {
        get => _movementCoroutine != null;
    }

    int _targetAngle;
    float _distance;

    private void Reset()
    {
        _movementSpeed = 1f;
    }

    private void Start()
    {
        _distance = Vector3.Distance(_crystal.position, transform.position);
        _targetAngle = 0;
    }

    public void SetNewDirection(Vector2 direction)
    {
        if (IsMoving) return;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, DETECTION_RANGE, Utilities.MovementLayers[_playerReflection.ReflectionColor]);
        if (!ray) return;
        if (ray.distance < 1f)
        {
            if (ray.collider.CompareTag("Mud"))
            {
                _movementCoroutine = StartCoroutine(MovementCoroutine(direction, ray));
            }
            else if (ray.collider.CompareTag("Crate"))
            {
                CheckCrateMovement(ray.transform, direction);
            }
        }
        _movementCoroutine = StartCoroutine(MovementCoroutine(direction, ray));
    }

    public void RotatePlayer(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _targetAngle += _eightLaserDirections ? 45 : 90;
            _targetAngle %= 360;
        }
        else
        {
            _targetAngle = Utilities.GetClosestInteger(Mathf.Atan2(direction.y, direction.x));
        }
        if (CheckNeededRotation()) return;
        if (_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
        }
        _rotationCoroutine = StartCoroutine(RotationCoroutine());
    }

    public bool CheckNeededRotation()
    {
        if (Mathf.Abs(((360 - _targetAngle) % 360) - _playerReflection.ForbiddenAngle) < ANGLE_TOLERANCE)
        {
            RotatePlayer(Vector2.zero);
            return true;
        }
        return false;
    }

    IEnumerator MovementCoroutine(Vector2 direction, RaycastHit2D raycast)
    {
        InputManager.Instance.CanMoveAPlayer = false;
        _onPlayerMoveStarted.Invoke();
        Vector3 initialPosition = transform.position;
        float distance = ((int)raycast.distance + (raycast.collider.CompareTag("Mud") ? 1 : 0));
        Vector3 positionToGo = initialPosition + (Vector3)direction * distance;
        float initialTime = Time.time;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue +
                _movementCurve.Evaluate(Mathf.Clamp(Time.time - initialTime, 0, _movementCurve.keys[_movementCurve.length - 1].time)
                / distance * _movementSpeed));
            transform.position = Vector3.Lerp(initialPosition, positionToGo, lerpValue);
            yield return null;
        }
        transform.position = positionToGo;
        CheckCrateMovement(raycast.transform, direction);
    }

    private void CheckCrateMovement(Transform hitObject, Vector2 direction)
    {
        if (hitObject.CompareTag("Crate") 
            && !Physics2D.OverlapCircle((Vector2)hitObject.position + direction, .45f, Utilities.MovementLayers[Utilities.GAMECOLORS.White]))
        {
            InputManager.Instance.CanMoveAPlayer = false;
            _moveCrateCoroutine = StartCoroutine(MoveCrateCoroutine(hitObject.gameObject, direction));
        }
        else
        {
            _movementCoroutine = null;
            InputManager.Instance.CanMoveAPlayer = true;
        }
    }


    private float LerpAngleUnclamped(float a, float b, float t)
    {
        float num = Mathf.Repeat(b - a, 360f);
        if (num > 180f)
        {
            num -= 360f;
        }
        return a + num * t;
    }

    IEnumerator RotationCoroutine()
    {
        _onPlayerRotationStarted.Invoke();
        float lerpValue = 0f;
        float initialAngle = Mathf.Atan2(_crystal.position.y -  transform.position.y,
            _crystal.position.x - transform.position.x) * Mathf.Rad2Deg;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue + (Time.deltaTime * _rotationSpeed));
            float lerpAngle = LerpAngleUnclamped(initialAngle, -_targetAngle, _rotationCurve.Evaluate(lerpValue));
            _crystal.position = new Vector3(Mathf.Cos(lerpAngle * Mathf.Deg2Rad) * _distance,
                Mathf.Sin(lerpAngle * Mathf.Deg2Rad) * _distance, 0) + transform.position;
            _crystal.rotation = Quaternion.Euler(0, 0, lerpAngle);
            yield return null;
        }
        _onPlayerRotationStopped.Invoke();
        _crystal.position = new Vector3(
            GetClosest(Mathf.Cos(-_targetAngle * Mathf.Deg2Rad)) * _distance,
            GetClosest(Mathf.Sin(-_targetAngle * Mathf.Deg2Rad)) * _distance, 0) 
            + transform.position;
        _crystal.rotation = Quaternion.Euler(0,0,-_targetAngle);
        _rotationCoroutine = null;
    }

    private float GetClosest(float value)
    {
        return Mathf.Abs(value) > 0.25f ? value : 0;
    }

    IEnumerator MoveCrateCoroutine(GameObject crate, Vector2 direction)
    {
        _onCrateMoveStart.Invoke();
        Vector3 initialPosition = crate.transform.position;
        CalculateCrateRay(direction, crate.transform.position);
        float distance = ((int)_crateRay.distance + (_crateRay.collider.CompareTag("Mud") ? 1 : 0));
        Vector3 positionToGo = initialPosition + (Vector3)direction * distance;
        float initialTime = Time.time;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue +
                _movementCurve.Evaluate(Mathf.Clamp(Time.time - initialTime, 0, _movementCurve.keys[_movementCurve.length - 1].time)
                / distance * _movementSpeed));
            crate.transform.position = Vector3.Lerp(initialPosition, positionToGo, lerpValue);
            yield return null;
        }
        _onCrateMoveStop.Invoke();
        _movementCoroutine = null;
        _moveCrateCoroutine = null;
        InputManager.Instance.CanMoveAPlayer = true;
    }

    private void CalculateCrateRay(Vector2 direction, Vector2 origin)
    {
        RaycastHit2D ray = Physics2D.Raycast(origin, direction, DETECTION_RANGE, Utilities.MovementLayers[_playerReflection.ReflectionColor]);
        if (!ray || (ray.distance < 1 && !ray.collider.CompareTag("Mud"))) return;
        _crateRay = ray;
    }
}
