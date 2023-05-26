using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float _swipeMinimumValue;

    PlayerController _playerController;

    private void Reset()
    {
        _swipeMinimumValue = 50f;
    }

    private void Update()
    {
        if (_playerController is null || Input.touches.Length != 1) return;
        Touch touch = Input.touches[0];
        if (touch.deltaPosition.magnitude < _swipeMinimumValue) return;
        if (Mathf.Abs(touch.deltaPosition.y) > Mathf.Abs(touch.deltaPosition.x))
        {
            if (touch.deltaPosition.y > 0)
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
            if (touch.deltaPosition.x > 0)
            {
                _playerController.SetNewDirection(Vector2.right);
            }
            else
            {
                _playerController.SetNewDirection(Vector2.left);
            }
        }
    }
}
