using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera _camera;

    [Header("Parameters")]
    [SerializeField, Range(-10f, 10f), OnValueChanged(nameof(ReplaceCamera))] float _bufferSize;
    [SerializeField, Range(-10f, 10f), OnValueChanged(nameof(ReplaceCamera))] float _offsetY;
    [SerializeField, Range(-1, 1), OnValueChanged(nameof(ReplaceCamera))] int _offsetX;

    private void ReplaceCamera()
    {
        Bounds bounds = new Bounds();
        foreach (var col in FindObjectsOfType<BoxCollider2D>())
        {
            bounds.Encapsulate(col.bounds);
        }
        bounds.Expand(_bufferSize);

        float size = Mathf.Max(bounds.size.y, bounds.size.x * _camera.pixelHeight / _camera.pixelWidth) * .5f;
        Vector3 center = bounds.center + new Vector3(_offsetX / 2f, _offsetY, -10f);
        _camera.transform.position = center;
        _camera.orthographicSize = size;
    }

    private void Start()
    {
        ReplaceCamera();
    }
}
