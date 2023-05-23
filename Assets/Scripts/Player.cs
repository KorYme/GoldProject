using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 _reflectPos;
    Vector3 _reflectDirection;

    private void Start()
    {
        _reflectPos = new Vector3(transform.position.x - 1, transform.position.y - 1);
    }

    private void Update()
    {
        foreach (Touch _touch in Input.touches)
        {
            if (Input.touchCount == 1)
            {
                if (_touch.phase == TouchPhase.Began)
                {
                    _reflectPos = GetDirection(_touch.position);
                }
                else if (_touch.phase == TouchPhase.Moved)
                {
                    _reflectPos = GetDirection(_touch.position);
                }
            }
        }
        Debug.Log(Input.touchCount);
    }

    private Vector2 GetDirection(Vector2 screenPosition)
    {
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    public Vector3 GetReflectDir() => _reflectPos;
}
