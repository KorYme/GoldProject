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
    [SerializeField] AnimationCurve _movementCurve;

    Coroutine _movementCoroutine;
    public bool IsMoving
    {
        get => _movementCoroutine != null;
    }

    private void Reset()
    {
        _speed = 1f;
    }

    public void SetNewDirection(Vector2 direction)
    {
        if (Physics2D.Raycast(transform.position, direction, 1, _wallLayer)) return;
        _movementCoroutine = StartCoroutine(MovementCoroutine(direction));
    }

    IEnumerator MovementCoroutine(Vector2 direction)
    {
        InputManager.Instance.CanMoveAPlayer = false;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, 20, _wallLayer);
        Vector2 initialPosition = transform.position;
        Vector2 positionToGo = initialPosition + direction * (int)raycast.distance;
        float initialTime = Time.time;
        float lerpValue = 0f;
        while (lerpValue < 1f)
        {
            lerpValue = Mathf.Clamp01(lerpValue + (_movementCurve.Evaluate((Time.time - initialTime) / (int)raycast.distance) * _speed));
            transform.position = Vector3.Lerp(initialPosition, positionToGo, lerpValue);
            yield return null;
        }
        Debug.Log("Fin de mouvement pour " + name);
        InputManager.Instance.CanMoveAPlayer = true;
        _movementCoroutine = null;
    }
}
