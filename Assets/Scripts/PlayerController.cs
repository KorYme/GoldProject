using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask _wallLayer;

    [Header("Parameters")]
    [SerializeField] float _swipeMinimumValue;
    [SerializeField, Tooltip("Number of unit moved by second")] float _speed;
    [SerializeField] Vector2 _movementRatio;
        
    float _lerpValue;
    Vector2 _initialPosition;
    Vector2 _currentDirection;
    Vector2 PositionToGo
    { 
        get => _initialPosition + new Vector2(_currentDirection.x * _movementRatio.x, _currentDirection.y * _movementRatio.y);
    }

    Coroutine _controllerCoroutine;
    public Coroutine ControllerCoroutine
    {
        get
        {
            return _controllerCoroutine;
        }
        private set
        {
            if (_controllerCoroutine != null) return;
            _controllerCoroutine = value;
        }
    }

    private void Reset()
    {
        _movementRatio = Vector2.one;
        _speed = 1f;
        _swipeMinimumValue = 50f;
    }

    private void Awake()
    {
        _lerpValue = 0;
        _initialPosition = transform.position;
    }

    private void Update()
    {
        ApplyMovement();
        if (Input.touches.Length != 1) return;
        Touch touch = Input.touches[0];
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
        if (Physics2D.Raycast(transform.position, _currentDirection, 1, _wallLayer))
        {
            _currentDirection = Vector2.zero;
        }
    }

    private void ApplyMovement()
    {
        if (_currentDirection == Vector2.zero) return;
        _lerpValue = Mathf.Clamp01(_lerpValue + (Time.deltaTime * _speed));
        transform.position = Vector3.Lerp(_initialPosition, PositionToGo, _lerpValue);
        if (_lerpValue == 1f)
        {
            //Check possibilité d'avancer
            if (Physics2D.Raycast(transform.position, _currentDirection, 1, _wallLayer))
            {
                _currentDirection = Vector2.zero;
            }
            _initialPosition = transform.position;
            _lerpValue = 0;
        }
    }

    public void GetNewDirection(Vector2 direction)
    {
        if (Physics2D.Raycast(transform.position, direction, 1, _wallLayer)) return;
        _currentDirection = direction;
    }
}
