using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask _wallLayer;

    [Header("Parameters")]
    [SerializeField, Tooltip("Number of unit moved by second")] float _speed;
    [SerializeField] Vector2 _movementRatio;
    [SerializeField] AnimationCurve _movementCurve;
        
    float _lerpValue;
    Vector2 _initialPosition;
    Vector2 _currentDirection;
    Vector2 PositionToGo
    { 
        get => _initialPosition + new Vector2(_currentDirection.x * _movementRatio.x, _currentDirection.y * _movementRatio.y);
    }

    bool _isMoving;
    public Action OnMovementStopped;
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            if (!value)
            {
                OnMovementStopped?.Invoke();
                OnMovementStopped = null;
            }
        }
    }

    private void Reset()
    {
        _movementRatio = Vector2.one;
        _speed = 1f;
    }

    private void Awake()
    {
        _lerpValue = 0;
        _initialPosition = transform.position;
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        // A MODIFIER
        if (_currentDirection == Vector2.zero) return;
        _lerpValue = Mathf.Clamp01(_lerpValue + (Time.deltaTime * _speed));
        transform.position = Vector3.Lerp(_initialPosition, PositionToGo, _lerpValue);
        if (_lerpValue == 1f)
        {
            if (Physics2D.Raycast(transform.position, _currentDirection, 1, _wallLayer))
            {
                _currentDirection = Vector2.zero;
                IsMoving = false;
            }
            _initialPosition = transform.position;
            _lerpValue = 0;
        }
    }

    public void SetNewDirection(Vector2 direction)
    {
        if (IsMoving || Physics2D.Raycast(transform.position, direction, 1, _wallLayer)) return; 
        _currentDirection = direction;
        IsMoving = true;
    }
}
