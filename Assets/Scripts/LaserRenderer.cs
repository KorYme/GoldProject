using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] List<Material> _materialsLaser;

    [Header("Parameters")]
    [SerializeField, Range(0f, 1f), OnValueChanged(nameof(ChangeValues))] float _laserWidth;
    [SerializeField, OnValueChanged(nameof(ChangeValues))] Utilities.GAMECOLORS _laserColor;

    public LineRenderer LineRenderer
    {
        get => _lineRenderer; 
    }

    private void Start()
    {
        ChangeValues();
    }

    public void ChangeLaserColor(Utilities.GAMECOLORS color)
    {
        if (_materialsLaser.Count <= (int)color) return;
        _laserColor = color;
        _lineRenderer.material = _materialsLaser[(int)color];
    }

    private void ChangeValues()
    {
        LineRenderer.startWidth = _laserWidth;
        LineRenderer.startWidth = _laserWidth;
        ChangeLaserColor(_laserColor);
    }

#if UNITY_EDITOR
    [Button]
    private void DebugLaserRenderer()
    {
        LineRenderer.SetPosition(1, LineRenderer.GetPosition(1) == Vector3.zero ? Vector3.right : Vector3.zero);
    }
#endif
}
