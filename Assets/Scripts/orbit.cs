using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    [SerializeField] Transform _origin;
    [SerializeField] float _speed;
    [SerializeField] float _distance;

    float angle = 0;

    void Update()
    {
        angle += Time.deltaTime * _speed;
        transform.position = new Vector3(Mathf.Cos(angle / 360 * 2 * Mathf.PI) * _distance, Mathf.Sin(angle / 360 * 2 * Mathf.PI) * _distance) + _origin.position;
        transform.LookAt(_origin.position);
    }
}
