using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLaser : MonoBehaviour
{
    [SerializeField] LaserDir _laserDir;

    Vector3 _rayCastDir;
    LineRenderer _lineRenderer;
    PlayerHitByRay _tempPlayer;
    PlayerHitByRay _currentPlayer;
    


    private void Start()
    {
        switch (_laserDir)
        {
            case LaserDir.Up:
                _rayCastDir = Vector3.up;
                break;
            case LaserDir.Down:
                _rayCastDir = Vector3.down;
                break;
            case LaserDir.Left:
                _rayCastDir = Vector3.left;
                break;
            case LaserDir.Right:
                _rayCastDir = Vector3.right;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _rayCastDir * 10000);

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
                    Debug.Log("Current player is null");
                }
                
                if (_tempPlayer != _currentPlayer)
                {
                    Debug.Log("Current player is not null");
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
        }
    }

    private enum LaserDir { Up, Down, Left, Right }
}