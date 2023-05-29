using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingLaser : MonoBehaviour
{
    private enum LaserDir
    {
        Up,
        UpRight,
        UpLeft,
        Down,
        DownRight,
        DownLeft,
        Left,
        Right
    }

    [Header("References")]
    [SerializeField] LineRenderer _lineRenderer;

    [Header("Parameters")]
    [SerializeField] LaserDir _laserDir;
    [SerializeField] LayersAndColors.GAMECOLORS _initialColor;

    Vector3 _raycastTarget;
    Reflectable _nextReflectable;

    private void Start()
    {
        _nextReflectable = null;
        switch (_laserDir)
        {
            case LaserDir.Up:
                _raycastTarget = Vector3.up;
                break;
            case LaserDir.Down:
                _raycastTarget = Vector3.down;
                break;
            case LaserDir.Left:
                _raycastTarget = Vector3.left;
                break;
            case LaserDir.Right:
                _raycastTarget = Vector3.right;
                break;
            case LaserDir.UpLeft:
                _raycastTarget = Vector3.up + Vector3.left;
                break;
            case LaserDir.UpRight:
                _raycastTarget = Vector3.up + Vector3.right;
                break;
            case LaserDir.DownLeft:
                _raycastTarget = Vector3.down + Vector3.left;
                break;
            case LaserDir.DownRight:
                _raycastTarget = Vector3.down + Vector3.right;
                break;
            default:
                break;
        }
        _raycastTarget.Normalize();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.startColor = LayersAndColors.GetColor(_initialColor);
        _lineRenderer.endColor = LayersAndColors.GetColor(_initialColor);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastTarget);
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hit.collider is null ? transform.position + (Vector3)(_raycastTarget * 100f) : hit.point);
        if (hit.collider == null) return;
        GameObject objectHit = hit.collider.gameObject;
        if (objectHit == (_nextReflectable?.gameObject ?? null)) return;
        _nextReflectable?.StopReflection();
        _nextReflectable = objectHit.GetComponent<Reflectable>();
        _nextReflectable?.StartReflection(_raycastTarget, _initialColor, hit);
    }
}