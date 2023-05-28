using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CrystalOrbitingPlayer : MonoBehaviour
{
    [SerializeField] Transform _origin;
    [SerializeField] float _speed;
    [SerializeField] float _distance;
    [SerializeField] float _angle = 0;
    [SerializeField] float _targetAngle = 0;
    [SerializeField] private bool _eightLaserDirections;

    bool _shouldMove;
    

    private void Start()
    {
    }

    void Update()
    {
        if (_shouldMove)
        {
            _origin.gameObject.GetComponent<PlayerHitByRay>().ShouldShootLaser = false;

            if (_targetAngle > _angle - 1f && _targetAngle < _angle + 1f)
            {
                switch (_targetAngle)
                {
                    case -45:
                        _angle = -45;
                        break;
                    case -90:
                        _angle = -90;
                        break;
                    case -135:
                        _angle = -135;
                        break;
                    case -180:
                        _angle = -180;
                        break;
                    case -225:
                        _angle = -225;
                        break;
                    case -270:
                        _angle = -270;
                        break;
                    case -315:
                        _angle = -315;
                        break;
                    case -360:
                        _angle = 0;
                        _targetAngle = 0;
                        break;
                    default:
                        break;
                }
                _shouldMove = false;
                _origin.gameObject.GetComponent<PlayerHitByRay>().ShouldShootLaser = true;
            }
            _angle -= Time.deltaTime * _speed;
            transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
        }

        foreach (Touch _touch in Input.touches)
        {
            if (Input.touchCount == 1)
            {
                if (_touch.phase == TouchPhase.Began)
                {
                    if (Camera.main.ScreenToWorldPoint(_touch.position).x > _origin.gameObject.transform.position.x - 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).x < _origin.gameObject.transform.position.x + 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).y > _origin.gameObject.transform.position.y - 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).y < _origin.gameObject.transform.position.y + 0.2f)
                    {
                        if (_eightLaserDirections)
                            _targetAngle -= 45;
                        else
                            _targetAngle -= 90;
                        
                        _shouldMove = true;
                    }
                }
                
                
            }
        }

        if (_angle < -360)
        {
            _shouldMove = false;
            _angle = 0;
        }
    }

    public void LaserBaseDirection(LineRenderer _lineRenderer)
    {
        if (_lineRenderer.GetPosition(0).x < _lineRenderer.GetPosition(1).x)
        {
            if (_lineRenderer.GetPosition(0).y < _lineRenderer.GetPosition(1).y)
            {
                _angle = -315;
                _targetAngle = -315;
            }
            else if (_lineRenderer.GetPosition(0).y > _lineRenderer.GetPosition(1).y)
            {
                _angle = -45;
                _targetAngle = -45;
            }
            else
            {
                _angle = 0;
                _targetAngle = 0;
            }
        }
        else if (_lineRenderer.GetPosition(0).x > _lineRenderer.GetPosition(1).x)
        {
            if (_lineRenderer.GetPosition(0).y < _lineRenderer.GetPosition(1).y)
            {
                _angle = -225;
                _targetAngle = -225;
            }
            else if (_lineRenderer.GetPosition(0).y > _lineRenderer.GetPosition(1).y)
            {
                _angle = -135;
                _targetAngle = -135;
            }
            else
            {
                _angle = -90;
                _targetAngle = -90;
            }
        }
        else
        {
            if (_lineRenderer.GetPosition(0).y < _lineRenderer.GetPosition(1).y)
            {
                _angle = -270;
                _targetAngle = -270;
            }
            else if (_lineRenderer.GetPosition(0).y > _lineRenderer.GetPosition(1).y)
            {
                _angle = -90;
                _targetAngle = -90;
            }
        }
        transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
    }
}
