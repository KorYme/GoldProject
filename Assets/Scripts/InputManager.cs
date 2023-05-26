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

    PlayerController _playerController;

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
        Debug.Log("Avant");
        if (collider2D is null) return;
        Debug.Log("Ca touche");
        _playerController = collider2D.GetComponent<PlayerController>();
        
    }

    private void OnInputPerformed(Finger finger)
    {
        if (_playerController is null) return;
        ETouch.Touch touch = finger.currentTouch;
        if (touch.delta.magnitude >= _swipeMinimumValue)
        {
            if (touch.delta.magnitude < _swipeMinimumValue) return;
            if (Mathf.Abs(touch.delta.y) > Mathf.Abs(touch.delta.x))
            {
                if (touch.delta.y > 0)
                {
                    _playerController.SetNewDirection(Vector2.up);
                }
                else
                {
                    _playerController.SetNewDirection(Vector2.down);
                }
            }
            else
            {
                if (touch.delta.x > 0)
                {
                    _playerController.SetNewDirection(Vector2.right);
                }
                else
                {
                    _playerController.SetNewDirection(Vector2.left);
                }
            }
            _playerController = null;
        }
    }

    private void OnInputStopped(Finger finger)
    {
        if (_playerController is null) return;
        Collider2D collider2D = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(finger.currentTouch.screenPosition), _checkSize, _playerLayer);
        if (collider2D is null) return;
        //Rotate the player
    }

    private void Update()
    {
        if (_playerController is null || Input.touches.Length != 1) return;

    }
}
