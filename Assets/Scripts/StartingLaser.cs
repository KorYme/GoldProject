using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StartingLaser : MonoBehaviour
{

    [Header("References")]
    [SerializeField] LaserRenderer _laserRenderer;
    [SerializeField] Reflectable _thisReflectable;

    [Header("Starting Laser Parameters")]
    [SerializeField] Utilities.DIRECTIONS _laserDir;
    [SerializeField] Utilities.GAMECOLORS _initialColor;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

    [SerializeField, Foldout("Colorblind")] ColorblindTextures _colorblindTextures;
    [SerializeField, Foldout("Colorblind")] Material _colorblindMaterial;

    [SerializeField] ParticleSystem _particlePrefab;

    ParticleSystem _pSystem;
    Vector3 _raycastTarget;
    Reflectable _nextReflectable;
    bool _colorblindModeEnabled = false;
    private void Reset()
    {
        ApplyParameters(false);
    }

    private void Start()
    {
        _nextReflectable = null;
        ChooseOrderInLayer(_laserDir);
        _raycastTarget = Utilities.GetDirection(_laserDir);
        _laserRenderer.LineRenderer.useWorldSpace = true;
        _laserRenderer.ChangeLaserColor(_initialColor);
        _colorblindModeEnabled = DataManager.Instance.ColorBlindModeEnabled;
        ApplyParameters(false);

        //Setup Particle system
        _pSystem = Instantiate(_particlePrefab);
        _pSystem.transform.position = transform.position;
        var main = _pSystem.main;
        main.startColor = Utilities.GetColor(_initialColor);
        _pSystem.transform.rotation = Quaternion.LookRotation(_raycastTarget *- 1);
        _pSystem.Play();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastTarget, 15f, Utilities.LightLayerMask);
        _laserRenderer.LineRenderer.SetPosition(0, transform.position);
        _laserRenderer.LineRenderer.SetPosition(1, hit.collider is null ? transform.position + (_raycastTarget * 100f) : hit.point);
        if (hit.collider == null) return;
        GameObject objectHit = hit.collider.gameObject;
        if (objectHit == (_nextReflectable?.gameObject ?? null)) return;
        _nextReflectable?.StopReflection();
        _nextReflectable = objectHit.GetComponent<Reflectable>();
        _laserRenderer.ChangeSecondPosition(hit.collider is null ? transform.position + (_raycastTarget * 100f) : hit.point, hit.normal ,_nextReflectable == null);
        _nextReflectable?.StartReflection(_raycastTarget, _initialColor, hit, _thisReflectable);
    }

    [Button]
    public void ApplyParameters(bool init = true)
    {
        if (_sprites.Count > (int)_initialColor && _sprites[(int)_initialColor] != null && !_colorblindModeEnabled)
        {
            _spriteRenderer.sprite = _sprites[(int)_initialColor];
            _spriteRenderer.color = Color.white;
        }
        else if (_colorblindModeEnabled)
        {
            _spriteRenderer.sprite = _sprites[0];
            _spriteRenderer.material = _colorblindMaterial;
            _spriteRenderer.material.SetTexture("_ColorTexture", _colorblindTextures.Textures[(int)_initialColor]);
        }
        else
        {
            _spriteRenderer.color = Utilities.GetColor(_initialColor);
        }
        if (init)   
        {
            FindObjectsOfType<StartingLaser>().Where(x => x != this).ToList().ForEach(x => x.ApplyParameters(false));
        }
    }

    private void ChooseOrderInLayer(Utilities.DIRECTIONS dir)
    {
        switch (dir)
        {
            case Utilities.DIRECTIONS.Up:
                GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Tiles";
                break;
            case Utilities.DIRECTIONS.Down:
                GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Tiles";
                break;
            default:
                break;
        }
    }
}