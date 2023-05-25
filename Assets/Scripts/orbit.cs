using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    [SerializeField] Transform _origin;
    [SerializeField] float _speed;
    [SerializeField] float _distance;

    [SerializeField] float _angle = 0;
    [SerializeField] float _targetAngle = 0;
    bool _shouldMove;

    [SerializeField] List<Transform> _positionArrows = new List<Transform>();


    private void Start()
    {
        
    }

    void Update()
    {
        if (_shouldMove)
        {
            //angle += Time.deltaTime * _speed;
            transform.position = new Vector3(Mathf.Cos(_angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(_angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;

            if (_targetAngle > _angle - 1f && _targetAngle < _angle + 1f)
            {
                _shouldMove = false;
            }

            if (_targetAngle > _angle)
            {
                _angle += Time.deltaTime * _speed;
            }
            else
            {
                _angle -= Time.deltaTime * _speed;
            }
        }

        foreach (Touch _touch in Input.touches)
        {
            if (Input.touchCount == 1)
            {
                if (Camera.main.ScreenToWorldPoint(_touch.position).x > _origin.gameObject.transform.position.x - 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).x < _origin.gameObject.transform.position.x + 0.2f)
                {
                    if (Camera.main.ScreenToWorldPoint(_touch.position).y > _origin.gameObject.transform.position.y - 0.2f && Camera.main.ScreenToWorldPoint(_touch.position).y < _origin.gameObject.transform.position.y + 0.2f)
                    {
                        TouchedPlayer();
                        Debug.Log("Touched");
                    }
                }
            }
        }

        if (_angle > 360)
        {
            _shouldMove = false;
            _angle = 0;
        }
    }

    private void TouchedPlayer()
    {
        _shouldMove = true;

        if (_angle < 10)
        {
            _targetAngle = 90;
        }
        else if (_angle > 80 && _angle < 100)
        {
            _targetAngle = 180;
        }
        else if (_angle > 140 && _angle < 200)
        {
            _targetAngle = 270;
        }
        else if (_angle > 260 && _angle < 290)
        {
            _targetAngle = 360;
        }
    }
}
