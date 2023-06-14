using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LaserRenderer : MonoBehaviour
{   
    [SerializeField] ParticleSystem _particleSystemPrefab;
    ParticleSystem _pSystem;

    [Header("References")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] List<Color> _colorsLaser;

    [Header("Parameters")]
    [SerializeField] float _intensity;
    [SerializeField, Range(0f, 1f), OnValueChanged(nameof(ChangeValues))] float _laserWidth;
    [SerializeField, OnValueChanged(nameof(ChangeValues))] Utilities.GAMECOLORS _laserColor;

    public LineRenderer LineRenderer
    {
        get => _lineRenderer;
    }

    private void Start()
    {
        ChangeValues();
        if (_pSystem == null)
        {
            _pSystem = Instantiate(_particleSystemPrefab);
            _pSystem.Stop();
        }
    }

    public void ChangeSecondPosition(Vector2 position, Vector2 normal, bool isWall = false)
    {
        if (isWall && _pSystem != null)
        {
            _pSystem.transform.position = position;
            _pSystem.transform.LookAt(_lineRenderer.GetPosition(0));
            var main = _pSystem.main;
            main.startColor = Utilities.GetColor(_laserColor);
            _pSystem.Play();
        }
        else if (!isWall)
        {
            _pSystem.Stop();
        }
        LineRenderer.SetPosition(1, position);
    }

    public void ChangeLaserColor(Utilities.GAMECOLORS color)
    {
        if (_colorsLaser.Count <= (int)color) return;
        _laserColor = color;
        float factor = Mathf.Pow(2, _intensity);
        Color colorToSet = new Color(_colorsLaser[(int)color].r * factor, _colorsLaser[(int)color].g * factor, _colorsLaser[(int)color].b * factor);
        _lineRenderer.material.SetColor("_Color", colorToSet);
        
    }

    private void ChangeValues()
    {
        LineRenderer.startWidth = _laserWidth;
        LineRenderer.startWidth = _laserWidth;
        ChangeLaserColor(_laserColor);
        if (_pSystem != null)
        {
            var main = _pSystem.main;
            main.startColor = Utilities.GetColor(_laserColor);
        }
        else
            Debug.Log("No particle system");
    }

#if UNITY_EDITOR
    [Button]
    private void DebugLaserRenderer()
    {
        LineRenderer.SetPosition(1, LineRenderer.GetPosition(1) == Vector3.zero ? Vector3.right : Vector3.zero);
    }
#endif
}
