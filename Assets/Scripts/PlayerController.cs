using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask _wallLayer;

    [Header("Parameters")]
    [SerializeField] float _speed;
    [SerializeField] int _unitMaxSpeed;
    [SerializeField] AnimationCurve _movementCurve;

    private void Reset()
    {
        _speed = 1f;
    }

    public void SetNewDirection(Vector2 direction)
    {
        if (Physics2D.Raycast(transform.position, direction, 1, _wallLayer)) return;
        StartCoroutine(MovementCoroutine(direction));
    }

    IEnumerator MovementCoroutine(Vector2 direction)
    {
        InputManager.Instance.CanMoveAPlayer = false;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, 20, _wallLayer);
        Vector2 initialPosition = transform.position;
        Vector2 positionToGo = initialPosition + direction * (int)raycast.distance;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue + ((Time.deltaTime * _speed) / (int)raycast.distance));
            transform.position = Vector3.Lerp(initialPosition, positionToGo, _movementCurve.Evaluate(lerpValue));
            yield return null;
        }
        InputManager.Instance.CanMoveAPlayer = true;
    }
}
