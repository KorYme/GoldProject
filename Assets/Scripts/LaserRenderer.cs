using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRenderer : MonoBehaviour
{   
    [SerializeField] ParticleSystem _particleSystemPrefab;
    ParticleSystem _pSystem;

    [Header("References")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] List<Material> _materialsLaser;
    [SerializeField] List<Color> _colorsLaser;

    [Header("Parameters")]
    [SerializeField, Range(0f, 1f), OnValueChanged(nameof(ChangeValues))] float _laserWidth;
    [SerializeField, OnValueChanged(nameof(ChangeValues))] Utilities.GAMECOLORS _laserColor;

    public LineRenderer LineRenderer
    {
        get => _lineRenderer;
    }
    bool _isTouchingWall;

    private void Start()
    {
        ChangeValues();
        _pSystem = Instantiate(_particleSystemPrefab);
        _pSystem.Stop();
    }

    public void ChangeSecondPosition(Vector2 position, Vector2 normal, bool isWall = false)
    {
        if (isWall)
        {
            _pSystem.transform.position = position;
            _pSystem.transform.LookAt(_lineRenderer.GetPosition(0));
            _pSystem.Play();
        }
        else if (!isWall)
        {
            _pSystem.Stop();
        }
        LineRenderer.SetPosition(1, position);
        //_isTouchingWall = isWall;
    }

    public void ChangeLaserColor(Utilities.GAMECOLORS color)
    {
        if (_colorsLaser.Count <= (int)color) return;
        _laserColor = color;
        _lineRenderer.material.SetColor("_Color", _colorsLaser[(int)color]);
    }

    private void ChangeValues()
    {
        LineRenderer.startWidth = _laserWidth;
        LineRenderer.startWidth = _laserWidth;
        ChangeLaserColor(_laserColor);
        if (_pSystem != null)
        _pSystem.startColor = Utilities.GetColor(_laserColor);

    }

#if UNITY_EDITOR
    [Button]
    private void DebugLaserRenderer()
    {
        LineRenderer.SetPosition(1, LineRenderer.GetPosition(1) == Vector3.zero ? Vector3.right : Vector3.zero);
    }
#endif
}
