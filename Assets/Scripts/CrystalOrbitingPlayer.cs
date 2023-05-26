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
    bool _shouldMove;
    public bool _eightLaserDirections;

    private void Start()
    {
        transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
    }

    void Update()
    {
        if (_shouldMove)
        {
            _angle += Time.deltaTime * _speed;
            transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
            _origin.gameObject.GetComponent<PlayerHitByRay>().ShouldShootLaser = false;

            if (_targetAngle > _angle - 1f && _targetAngle < _angle + 1f)
            {
                _shouldMove = false;
                _origin.gameObject.GetComponent<PlayerHitByRay>().ShouldShootLaser = true;
            }
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

        if (_angle < -357)
        {
            _shouldMove = false;
            _angle = 0;
            _targetAngle = 0;
        }
    }
}
