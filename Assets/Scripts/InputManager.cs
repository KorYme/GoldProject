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
    bool _canMoveAPlayer;

    private void Reset()
    {
        _swipeMinimumValue = 50f;
        _checkSize = .1f;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnInputStarted;
        ETouch.Touch.onFingerMove += OnInputPerformed;
        ETouch.Touch.onFingerUp += OnInputStopped;
        _canMoveAPlayer = true;
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
        Collider2D collider2D = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(finger.currentTouch.screenPosition), _checkSize, _playerLayer);
        if (collider2D is null) return;
        _currentPlayerTouched = collider2D.GetComponent<PlayerController>();
        _currentPlayerTouched.OnMovementStopped += () => _canMoveAPlayer = true;
    }

    private void OnInputPerformed(Finger finger)
    {
        if (!_canMoveAPlayer || _currentPlayerTouched is null) return;
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
        //Rotate the player
    }
}
