using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class orbit : MonoBehaviour
{
    [SerializeField] Transform _origin;
    [SerializeField] float _speed;
    [SerializeField] float _distance;

    [SerializeField] float _angle = 0;
    [SerializeField] float _targetAngle = 0;
    bool _shouldMove;
    public bool _movesInReverse;

    //[SerializeField] List<Transform> _positionArrows = new List<Transform>();


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

            if (_targetAngle > _angle - 3f && _targetAngle < _angle + 3f)
            {
                _shouldMove = false;
            }

            //if (_movesInReverse)
            //    _angle -= Time.deltaTime * _speed;
            //else
            //    _angle += Time.deltaTime * _speed;
        }

        foreach (Touch _touch in Input.touches)
        {
            if (Input.touchCount == 1)
            {
                if (_touch.phase == TouchPhase.Began)
                {
                    if (Camera.main.ScreenToWorldPoint(_touch.position).x > _origin.gameObject.transform.position.x - 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).x < _origin.gameObject.transform.position.x + 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).y > _origin.gameObject.transform.position.y - 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).y < _origin.gameObject.transform.position.y + 0.2f)
                    {
                        _targetAngle += 90;
                        _shouldMove = true;
                    }
                    //foreach (Transform _arrow in _positionArrows)
                    //{
                    //    if (Camera.main.ScreenToWorldPoint(_touch.position).x > _arrow.gameObject.transform.position.x - 0.1f && Camera.main.ScreenToWorldPoint(_touch.position).x < _arrow.gameObject.transform.position.x + 0.1f && Camera.main.ScreenToWorldPoint(_touch.position).y > _arrow.gameObject.transform.position.y - 0.1f && Camera.main.ScreenToWorldPoint(_touch.position).y < _arrow.gameObject.transform.position.y + 0.1f)
                    //    {
                    //        switch (_arrow.gameObject.name)
                    //        {
                    //            case "top":
                    //                _targetAngle = 90;
                    //                if (_angle < 80)
                    //                    _movesInReverse = false;
                    //                else
                    //                    _movesInReverse = true;
                    //                _shouldMove = true;
                    //                break;
                    //            case "bottom":
                    //                _targetAngle = 270;
                    //                if (_angle > 170)
                    //                    _movesInReverse = false;
                    //                else
                    //                    _movesInReverse = true;
                    //                _shouldMove = true;
                    //                break;
                    //            case "left":
                    //                _targetAngle = 180;
                    //                if (_angle > 80)
                    //                    _movesInReverse = false;
                    //                else
                    //                    _movesInReverse = true;
                    //                _shouldMove = true;
                    //                break;
                    //            case "right":
                    //                if (_angle > 200)
                    //                {
                    //                    _movesInReverse = false;
                    //                    _targetAngle = 360;
                    //                }
                    //                else
                    //                {
                    //                    _movesInReverse = true;
                    //                    _targetAngle = 0;
                    //                }

                    //                _shouldMove = true;
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //    }
                    //}
                }
                
                
            }
        }

        if (_angle > 357)
        {
            _shouldMove = false;
            _angle = 0;
            _targetAngle = 0;
        }
    }
}
