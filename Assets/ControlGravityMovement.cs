using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ControlGravityMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask _wallLayer;

    [Header("Parameters")]
    [SerializeField] float _swipeMinimumValue;
    [SerializeField] float _timePerUnit;
    [SerializeField] Vector2 _movementRatio;

    Vector2 _initialPosition;
    float _lerpValue;

    Vector2 PositionToGo
    { 
        get => _initialPosition + new Vector2(_currentDirection.x * _movementRatio.x, _currentDirection.y * _movementRatio.y);
    }

    Vector2 _currentDirection;

    private void Reset()
    {
        _movementRatio = Vector2.one;
        _timePerUnit = 1;
    }

    private void Awake()
    {
        _lerpValue = 0;
    }

    private void Update()
    {
        ApplyMovement();
        if (Input.touches.Length != 1) return;
        UnityEngine.Touch touch = Input.touches[0];
        if (_currentDirection.magnitude > 0.01f) return;
        if (touch.deltaPosition.magnitude < _swipeMinimumValue) return;
        if (Mathf.Abs(touch.deltaPosition.y) > Mathf.Abs(touch.deltaPosition.x))
        {
            if (touch.deltaPosition.y > 0)
            {
                _currentDirection = Vector2.up;
            }
            else
            {
                _currentDirection = Vector2.down;
            }
        }
        else
        {
            if (touch.deltaPosition.x > 0)
            {
                _currentDirection = Vector2.right;
            }
            else
            {
                _currentDirection = Vector2.left;
            }
        }
    }

    private void ApplyMovement()
    {
        if (_currentDirection == Vector2.zero) return;
        _lerpValue = Mathf.Clamp01(_lerpValue + (Time.deltaTime / _timePerUnit));
        transform.position = Vector3.Lerp(_initialPosition, PositionToGo, _lerpValue);
        if (_lerpValue == 1f)
        {
            //Check possibilité d'avancer
            if (true)
            {

            }
            _lerpValue = 0;
        }
    }
}
