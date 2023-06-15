using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera _camera;
    [SerializeField] List<TileBehaviour> _exceptionObjects;

    [Header("Parameters")]
    [SerializeField, Range(-10f, 10f), OnValueChanged(nameof(ReplaceCamera))] float _bufferSize;

    [Button]
    private void ReplaceCamera()
    {
        Bounds bounds = new Bounds();
        foreach (var tile in FindObjectsOfType<TileBehaviour>().Where(x => x.Type != TileBehaviour.TileType.Border 
        && x.Type != TileBehaviour.TileType.LaserStart))
        {
            if (_exceptionObjects.Contains(tile)) continue;
            bounds.Encapsulate(tile.BoxCollider2D.bounds);
        }
        bounds.Expand(_bufferSize);

        float size = Mathf.Max(bounds.size.y, bounds.size.x * _camera.pixelHeight / _camera.pixelWidth) * .5f;
        Vector3 center = bounds.center + new Vector3(0f, 0f, -10f);
        _camera.transform.position = center;
        _camera.orthographicSize = size;
    }

    private void Start()
    {
        ReplaceCamera();
    }
}
