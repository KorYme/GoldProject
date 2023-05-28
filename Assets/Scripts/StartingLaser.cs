using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingLaser : MonoBehaviour
{
    [SerializeField] LaserDir _laserDir;

    LineRenderer _lineRenderer;
    PlayerHitByRay _tempPlayer;
    PlayerHitByRay _currentPlayer;
    Vector3 _raycastTarget;
    private int _angle;
    private float _distance;

    private void Start()
    {
        switch (_laserDir)
        {
            case LaserDir.Up:
                _angle = -270;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.Down:
                _angle = 90;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.Left:
                _angle = -180;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.Right:
                _angle = 0;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.UpLeft:
                _angle = _angle = -225;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.UpRight:
                _angle = -315;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.DownLeft:
                _angle = -135;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            case LaserDir.DownRight:
                _angle = -45;
                _raycastTarget = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + transform.position;
                break;
            default:
                break;
        }
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.startColor = Color.blue;
        _lineRenderer.endColor = Color.blue;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastTarget - transform.position * 10000);

        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                _tempPlayer = hit.collider.gameObject.GetComponent<PlayerHitByRay>();
                if (_currentPlayer == null)
                {
                    _currentPlayer = _tempPlayer;
                }
                
                if (_tempPlayer != _currentPlayer)
                {
                    _currentPlayer.HitByRay(_lineRenderer, false);
                    _currentPlayer = _tempPlayer;
                    _currentPlayer.HitByRay(_lineRenderer, true);
                }
                else
                {
                    _currentPlayer.HitByRay(_lineRenderer, true);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Target"))
            {
               
                //Do whatever we want to do with the target of the laser
            }
            else
            {
                _currentPlayer.HitByRay(_lineRenderer, false);
            }
        }
    }

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
}