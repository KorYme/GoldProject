using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GyroscopeTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ConstantForce2D _constantForce;

    private void Awake()
    {
        Input.gyro.enabled = true;
        _constantForce.force = new Vector2(0,0);
    }

    private void Update()
    {
        _rb.gravityScale = 0;
        Debug.Log(Input.gyro.gravity);
        _constantForce.force = new Vector2(Input.gyro.gravity.normalized.x * 9.8f, Input.gyro.gravity.normalized.y * 9.8f);
    }
}
