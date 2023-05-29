using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingLaser : MonoBehaviour
{
    [SerializeField] LaserDir _laserDir;

    LineRenderer _lineRenderer;
    GameObject _tempPlayer;
    GameObject _currentPlayer;
    Vector3 _raycastTarget;

    private void Start()
    {
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
        _raycastTarget += transform.position;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.startColor = Color.blue;
        _lineRenderer.endColor = Color.blue;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastTarget - transform.position);
        
        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                _tempPlayer = hit.collider.gameObject;
                if (_currentPlayer == null)
                {
                    _currentPlayer = _tempPlayer;
                }
                
                if (_tempPlayer != _currentPlayer)
                {
                    _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, false);
                    _currentPlayer = _tempPlayer;
                    _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, true);
                }
                else
                {
                    _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, true);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Mirror"))
            {
                _tempPlayer = hit.collider.gameObject;
                if (_currentPlayer == null)
                {
                    _currentPlayer = _tempPlayer;
                }

                if (_tempPlayer != _currentPlayer)
                {
                    _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer,hit.point , false);
                    _currentPlayer = _tempPlayer;
                    _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, true);
                }
                else
                {
                    _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, true);
                }
            }
            else
            {
                if (_currentPlayer != null)
                {
                    if (_currentPlayer.GetComponent<Mirror>() != null)
                    {
                        _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, false);
                        _currentPlayer = null;
                    }
                    else if (_currentPlayer.GetComponent<PlayerHitByRay>() != null)
                    {
                        _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, false);
                        _currentPlayer = null;
                    }
                        
                }
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