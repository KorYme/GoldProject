using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class InputManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float _swipeMinimumValue;
    [SerializeField] float _checkSize;
    [SerializeField] LayerMask _playerLayer;

    PlayerController _currentPlayerTouched;
    public bool CanMoveAPlayer
    {
        get;
        set;
    }

    private void Reset()
    {
        _swipeMinimumValue = 50f;
        _checkSize = .1f;
    }

    public static InputManager Instance;

    private void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnInputStarted;
        ETouch.Touch.onFingerMove += OnInputPerformed;
        ETouch.Touch.onFingerUp += OnInputStopped;
        CanMoveAPlayer = true;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnInputStarted;
        ETouch.Touch.onFingerMove -= OnInputPerformed;
        ETouch.Touch.onFingerUp -= OnInputStopped;
        EnhancedTouchSupport.Disable();
    }
    
    private void OnInputStarted(Finger finger)
    {
        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(finger.currentTouch.screenPosition);
        Collider2D collider2D = GetClosestCollider(Physics2D.OverlapCircleAll(fingerPosition,
            _checkSize, _playerLayer), fingerPosition);
        if (collider2D is null) return;
        _currentPlayerTouched = collider2D.GetComponent<PlayerController>();
        if (_currentPlayerTouched is null) return;
    }

    private void OnInputPerformed(Finger finger)
    {
        if (!CanMoveAPlayer || _currentPlayerTouched is null) return;
        ETouch.Touch touch = finger.currentTouch;
        if (touch.delta.magnitude < _swipeMinimumValue) return;
        if (Mathf.Abs(touch.delta.y) > Mathf.Abs(touch.delta.x))
        {
            if (touch.delta.y > 0)
            {
                _currentPlayerTouched.SetNewDirection(Vector2.up);
            }
            else
            {
                _currentPlayerTouched.SetNewDirection(Vector2.down);
            }
        }
        else
        {
            if (touch.delta.x > 0)
            {
                _currentPlayerTouched.SetNewDirection(Vector2.right);
            }
            else
            {
                _currentPlayerTouched.SetNewDirection(Vector2.left);
            }
        }
        _currentPlayerTouched = null;
    }

    private void OnInputStopped(Finger finger)
    {
        if (_currentPlayerTouched is null) return;
        Collider2D collider2D = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(finger.currentTouch.screenPosition), _checkSize, _playerLayer);
        if (collider2D is null) return;
        if (collider2D.GetComponent<PlayerController>() == _currentPlayerTouched)
        {
            _currentPlayerTouched.RotatePlayer(Vector2.zero);
        }
        _currentPlayerTouched = null;
    }

    private Collider2D GetClosestCollider(Collider2D[] colliders, Vector2 position)
    {
        if (colliders is null || colliders.Length == 0) return null;
        int closestIndex = 0;
        for (int i = 1; i < colliders.Length; i++)
        {
            if (Vector2.Distance(colliders[i].transform.position, position)
                < Vector2.Distance(colliders[closestIndex].transform.position, position))
            {
                closestIndex = i;
            }
        }
        return colliders[closestIndex];
    }
}