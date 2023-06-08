using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    const float DETECTION_RANGE = 20f;

    [Header("References")]
    [SerializeField] Transform _crystal;
    [SerializeField] PlayerReflection _playerReflection;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _moveableGFX;
    [SerializeField] AnimatorManager _animatorManager;

    [Header("Movement Parameters")]
    [SerializeField] float _movementSpeed;
    [SerializeField, Tooltip("Acceleration over time")] AnimationCurve _movementCurve;

    [Header("Rotation Parameters")]
    [SerializeField, OnValueChanged(nameof(SetUpCrystalPosition))] Utilities.DIRECTIONS _initialDirection;
    [SerializeField, Range(0f,1f), OnValueChanged(nameof(SetUpCrystalPosition))] float _distance;
    [SerializeField] float _rotationSpeed;
    [SerializeField] bool _eightLaserDirections;
    [SerializeField, Tooltip("Movement curve of the crystal")] AnimationCurve _rotationCurve;

    [Space(10f)]
    [SerializeField, Foldout("Events")] UnityEvent _onPlayerMoveStarted;
    [SerializeField, Foldout("Events")] UnityEvent _onPlayerMoveStopped;
    [SerializeField, Foldout("Events")] UnityEvent _onPlayerRotationStarted;
    [SerializeField, Foldout("Events")] UnityEvent _onPlayerRotationStopped;
    [SerializeField, Foldout("Events")] UnityEvent _onCrateMoveStart;
    [SerializeField, Foldout("Events")] UnityEvent _onCrateMoveStop;

    Coroutine _movementCoroutine;
    Coroutine _rotationCoroutine;
    Coroutine _moveCrateCoroutine;
    Coroutine _wallHitCoroutine;
    RaycastHit2D _crateRay;

    const float ANGLE_TOLERANCE = 2f;
    public bool IsMoving
    {
        get => _movementCoroutine != null || _moveCrateCoroutine != null;
    }

    public bool IsHittingWall
    {
        get => _wallHitCoroutine != null;
    }

    public bool IsCrateMoving
    {
        get; private set;
    }
    public bool IsRotating
    {
        get; private set;
    }

    int _targetAngle;

    private void Reset()
    {
        _movementSpeed = 1f;
    }

    private void Start()
    {
        Vector2 crystalDirection = _crystal.position - transform.position;
        _targetAngle = Utilities.GetClosestInteger(Mathf.Atan2(crystalDirection.y, crystalDirection.x) * Mathf.Rad2Deg);
    }

    private void SetUpCrystalPosition()
    {
        _crystal.position = (Vector3)Utilities.GetDirection(_initialDirection) * _distance + transform.position;
        Vector2 crystalDirection = _crystal.position - transform.position;
        _crystal.rotation = Quaternion.Euler(0, 0, Utilities.GetClosestInteger(Mathf.Atan2(crystalDirection.y, crystalDirection.x) * Mathf.Rad2Deg));
    }

    public void SetNewDirection(Vector2 direction)
    {
        if (!InputManager.Instance.CanMoveAPlayer) return;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, DETECTION_RANGE, Utilities.MovementLayers[_playerReflection.ReflectionColor]);
        if (!ray) return;
        RaycastHit2D[] rays = Physics2D.RaycastAll(transform.position, direction, 1.25f, Utilities.MovementLayers[_playerReflection.ReflectionColor]);
        foreach (RaycastHit2D item in rays)
        {
            if (item.transform.CompareTag("Player")) return;
        }
        if (ray.distance < 1f)
        {
            if (ray.collider.CompareTag("Mud"))
            {
                _movementCoroutine = StartCoroutine(MovementCoroutine(direction, ray));
                InputManager.Instance.MovementNumber++;
            }
            else if (ray.collider.CompareTag("Crate"))
            {
                if (CheckCrateMovement(ray.transform, direction))
                {
                    InputManager.Instance.MovementNumber++;
                }
            }
        }
        else
        {
            _movementCoroutine = StartCoroutine(MovementCoroutine(direction, ray));
            InputManager.Instance.MovementNumber++;
        }
    }

    public void RotateCrystal(Vector2 direction)
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
            RotateCrystal(Vector2.zero);
            return true;
        }
        return false;
    }

    IEnumerator MovementCoroutine(Vector2 direction, RaycastHit2D raycast)
    {
        if (_wallHitCoroutine != null)
        {
            StopCoroutine(_wallHitCoroutine);
        }
        InputManager.Instance.CanMoveAPlayer = false;
        _moveableGFX.rotation = Quaternion.Euler(0, direction.x > 0.1f ? 180 : 0, 0);
        if (direction.y == 0f)
        {
            _animatorManager.ChangeAnimation(ANIMATION_STATES.Slide_profil);
        }
        else if (direction.y > 0f)
        {
            _animatorManager.ChangeAnimation(ANIMATION_STATES.Slide_dos);
        }
        else
        {
            _animatorManager.ChangeAnimation(ANIMATION_STATES.Slide_face);
        }
        _onPlayerMoveStarted?.Invoke();
        Vector3 initialPosition = transform.position;
        float distance = ((int)raycast.distance + (raycast.collider.CompareTag("Mud") ? 1 : 0));
        Vector3 positionToGo = initialPosition + (Vector3)direction * distance;
        float initialTime = Time.time;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue +
                _movementCurve.Evaluate(Mathf.Clamp(Time.time - initialTime, 0, _movementCurve.keys[_movementCurve.length - 1].time)
                / distance * _movementSpeed) * Time.deltaTime);
            transform.position = Vector3.Lerp(initialPosition, positionToGo, lerpValue);
            yield return null;
        }
        _onPlayerMoveStopped?.Invoke();
        if (direction.y == 0f)
        {
            _wallHitCoroutine = StartCoroutine(WallHitCoroutine(_animatorManager.ChangeAnimation(ANIMATION_STATES.Hit_profil)));
        }
        else if (direction.y > 0)
        {
            _wallHitCoroutine = StartCoroutine(WallHitCoroutine(_animatorManager.ChangeAnimation(ANIMATION_STATES.Hit_dos)));
        }
        else
        {
            _wallHitCoroutine = StartCoroutine(WallHitCoroutine(_animatorManager.ChangeAnimation(ANIMATION_STATES.Hit_face)));
        }
        transform.position = positionToGo;
        CheckCrateMovement(raycast.transform, direction);
        _movementCoroutine = null;
    }

    private bool CheckCrateMovement(Transform hitObject, Vector2 direction)
    {
        if (hitObject.CompareTag("Crate") 
            && !Physics2D.OverlapCircle((Vector2)hitObject.position + direction, .45f, Utilities.MovementLayers[Utilities.GAMECOLORS.White]))
        {
            InputManager.Instance.CanMoveAPlayer = false;
            hitObject.GetComponentInChildren<Animator>()?.SetTrigger(direction.x != 0 ? "HorizontalMovement" : "VerticalMovement");
            _moveCrateCoroutine = StartCoroutine(MoveCrateCoroutine(hitObject.gameObject, direction));
            return true;
        }
        else
        {
            InputManager.Instance.CanMoveAPlayer = true;
            return false;
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
        IsRotating = true;
        _onPlayerRotationStarted?.Invoke();
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
        IsRotating = false;
        _onPlayerRotationStopped?.Invoke();
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
        IsCrateMoving = true;
        _onCrateMoveStart?.Invoke();
        Vector3 initialPosition = crate.transform.position;
        CalculateCrateRay(direction, crate.transform.position);
        float distance = ((int)_crateRay.distance + (_crateRay.collider.CompareTag("Mud") ? 1 : 0));
        Vector3 positionToGo = initialPosition + (Vector3)direction * distance;
        float initialTime = Time.time;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue +
                _movementCurve.Evaluate(Mathf.Clamp(Time.time - initialTime, 0, _movementCurve.keys[_movementCurve.length - 1].time) / distance
                * _movementSpeed * Time.deltaTime));
            crate.transform.position = Vector3.Lerp(initialPosition, positionToGo, lerpValue);
            yield return null;
        }
        crate.GetComponentInChildren<Animator>()?.SetTrigger("StopMovement");
        IsCrateMoving = false;
        _onCrateMoveStop?.Invoke();
        _moveCrateCoroutine = null;
        InputManager.Instance.CanMoveAPlayer = true;
    }

    private void CalculateCrateRay(Vector2 direction, Vector2 origin)
    {
        RaycastHit2D ray = Physics2D.Raycast(origin, direction, DETECTION_RANGE, Utilities.MovementLayers[_playerReflection.ReflectionColor]);
        if (!ray || (ray.distance < 1 && !ray.collider.CompareTag("Mud"))) return;
        _crateRay = ray;
    }

    IEnumerator WallHitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _moveableGFX.rotation = Quaternion.Euler(0, 0, 0);
        _animatorManager.ChangeAnimation(_playerReflection.IsReflecting ? ANIMATION_STATES.Reflection : ANIMATION_STATES.Idle);
        _wallHitCoroutine = null;
    }
}
