using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] Transform _crystal;
    [SerializeField] PlayerReflection _playerReflection;

    [Header("Movement Parameters")]
    [SerializeField] float _movementSpeed;
    [SerializeField, Tooltip("Acceleration over time")] AnimationCurve _movementCurve;

    [Header("Rotation Parameters")]
    [SerializeField] float _rotationSpeed;
    [SerializeField] bool _eightLaserDirections;
    [SerializeField, Tooltip("Movement curve of the crystal")] AnimationCurve _rotationCurve;
    
    Coroutine _movementCoroutine;
    Coroutine _rotationCoroutine;

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
        if (Physics2D.Raycast(transform.position, direction, 1, _wallLayer)) return;
        _movementCoroutine = StartCoroutine(MovementCoroutine(direction));
    }

    public void RotatePlayer(Vector2 direction)
    {
        Debug.Log("RotatePlayer");
        if (direction == Vector2.zero)
        {
            _targetAngle += _eightLaserDirections ? 45 : 90;
            _targetAngle %= 360;
        }
        else
        {
            _targetAngle = ((int)(Mathf.Atan2(direction.y, direction.x) + Mathf.Epsilon) + 360 ) % 360;
        }
        if (_targetAngle == _playerReflection.ForbiddenAngle)
        {
            RotatePlayer(Vector2.zero);
            return;
        }
        if (_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
        }
        _rotationCoroutine = StartCoroutine(RotationCoroutine());
    }

    IEnumerator MovementCoroutine(Vector2 direction)
    {
        InputManager.Instance.CanMoveAPlayer = false;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, 20, LayersAndColors.MovementLayers[_playerReflection.ReflectionColor]);
        float distance = (int)raycast.distance;
        if (raycast.collider.tag == "Mud")
        {
            distance++;
        }
        Vector2 initialPosition = transform.position;
        Vector2 positionToGo = initialPosition + direction * (int)raycast.distance;
        float initialTime = Time.time;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue + 
                (_movementCurve.Evaluate(Mathf.Clamp(Time.time - initialTime, 0, _movementCurve.keys[_movementCurve.length - 1].time) 
                / (int)raycast.distance) * _movementSpeed));
            transform.position = Vector3.Lerp(initialPosition, positionToGo, lerpValue);
            yield return null;
        }
        InputManager.Instance.CanMoveAPlayer = true;
        _movementCoroutine = null;
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
        float lerpValue = 0f;
        float initialAngle = Mathf.Atan2(_crystal.position.y -  transform.position.y,
            _crystal.position.x - transform.position.x) * Mathf.Rad2Deg;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue + (Time.deltaTime * _rotationSpeed));
            float lerpAngle = LerpAngleUnclamped(initialAngle, -_targetAngle, _rotationCurve.Evaluate(lerpValue));
            _crystal.position = new Vector3(Mathf.Cos(lerpAngle * Mathf.Deg2Rad) * _distance,
                Mathf.Sin(lerpAngle * Mathf.Deg2Rad) * _distance, 0) + transform.position;
            yield return null;
        }
        _rotationCoroutine = null;
    }
}
