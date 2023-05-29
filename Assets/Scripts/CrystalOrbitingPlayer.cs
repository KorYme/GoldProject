using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CrystalOrbitingPlayer : MonoBehaviour
{
    [SerializeField] Transform _origin;
    [SerializeField] float _speed;
    [SerializeField] float _angle = 0;
    [SerializeField] float _targetAngle = 0;
    [SerializeField] private bool _eightLaserDirections;

    float _distance;
    bool _shouldRotate;

    private void Start()
    {
        _distance = Vector3.Distance(_origin.position, transform.position);
    }

    //void Update()
    //{
    //    if (_shouldRotate)
    //    {
    //        _origin.gameObject.GetComponent<PlayerHitByRay>().ShouldShootLaser = false;
    //        if (_targetAngle > _angle - 1f && _targetAngle < _angle + 1f)
    //        {
    //            switch (_targetAngle)
    //            {
    //                case -45:
    //                    _angle = -45;
    //                    break;
    //                case -90:
    //                    _angle = -90;
    //                    break;
    //                case -135:
    //                    _angle = -135;
    //                    break;
    //                case -180:
    //                    _angle = -180;
    //                    break;
    //                case -225:
    //                    _angle = -225;
    //                    break;
    //                case -270:
    //                    _angle = -270;
    //                    break;
    //                case -315:
    //                    _angle = -315;
    //                    break;
    //                case -360:
    //                    _angle = 0;
    //                    _targetAngle = 0;
    //                    break;
    //                default:
    //                    break;
    //            }
    //            _shouldRotate = false;
    //            _origin.gameObject.GetComponent<PlayerHitByRay>().ShouldShootLaser = true;
    //        }
    //        _angle -= Time.deltaTime * _speed;
    //        transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
    //    }
    //    if (_angle < -360)
    //    {
    //        _shouldRotate = false;
    //        _angle = 0;
    //    }
    //}

    public void LaserBaseDirection(LineRenderer _lineRenderer)
    {
        if (_lineRenderer.GetPosition(0).x < _lineRenderer.GetPosition(1).x)
        {
            if (_lineRenderer.GetPosition(0).y < _lineRenderer.GetPosition(1).y && !(Mathf.Abs(_lineRenderer.GetPosition(0).y - _lineRenderer.GetPosition(1).y) < 0.1f))
            {
                _angle = -315;
                _targetAngle = -315;
            }
            else if (_lineRenderer.GetPosition(0).y > _lineRenderer.GetPosition(1).y && !(Mathf.Abs(_lineRenderer.GetPosition(0).y - _lineRenderer.GetPosition(1).y) < 0.1))
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
            if (_lineRenderer.GetPosition(0).y < _lineRenderer.GetPosition(1).y && !(Mathf.Abs(_lineRenderer.GetPosition(0).y - _lineRenderer.GetPosition(1).y) < 0.1))
            {
                _angle = -225;
                _targetAngle = -225;
            }
            else if (_lineRenderer.GetPosition(0).y > _lineRenderer.GetPosition(1).y && !(Mathf.Abs(_lineRenderer.GetPosition(0).y - _lineRenderer.GetPosition(1).y) < 0.1))
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
            if (_lineRenderer.GetPosition(0).y < _lineRenderer.GetPosition(1).y && (Mathf.Abs(_lineRenderer.GetPosition(0).y - _lineRenderer.GetPosition(1).y) < 0.1))
            {
                _angle = -270;
                _targetAngle = -270;
            }
            else if (_lineRenderer.GetPosition(0).y > _lineRenderer.GetPosition(1).y && (Mathf.Abs(_lineRenderer.GetPosition(0).y - _lineRenderer.GetPosition(1).y) < 0.1))
            {
                _angle = -90;
                _targetAngle = -90;
            }
        }
        transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
    }
}