using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 _mouseRaw;
    Vector3 _mousePos;

    private void Update()
    {
        _mouseRaw = Input.mousePosition;
        _mouseRaw.z = 10;
        _mousePos = Camera.main.ScreenToWorldPoint(_mouseRaw);
    }

    public Vector3 GetMousePos() => _mousePos;
}
