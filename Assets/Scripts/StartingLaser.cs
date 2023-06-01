using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StartingLaser : MonoBehaviour
{
    private enum LaserDir
    {
        Up,
        Down,
        Left,
        Right
    }

    [Header("References")]
    [SerializeField] LaserRenderer _laserRenderer;

    [Header("Starting Laser Parameters")]
    [SerializeField] LaserDir _laserDir;
    [SerializeField] LayersAndColors.GAMECOLORS _initialColor;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

    Vector3 _raycastTarget;
    Reflectable _nextReflectable;

    private void Reset()
    {
        ApplyParameters(false);
    }

    private void Start()
    {
        _nextReflectable = null;
        switch (_laserDir)
        {
            case LaserDir.Up:
                _raycastTarget = Vector3.up;
                break;
            case LaserDir.Down:
                _raycastTarget = Vector3.down;
                break;
            case LaserDir.Left:
                _raycastTarget = Vector3.left;
                break;
            case LaserDir.Right:
                _raycastTarget = Vector3.right;
                break;
            default:
                break;
        }
        _raycastTarget.Normalize();
        _laserRenderer.LineRenderer.useWorldSpace = true;
        _laserRenderer.LineRenderer.startWidth = 0.08f;
        _laserRenderer.LineRenderer.endWidth = 0.08f;
        _laserRenderer.LineRenderer.startColor = LayersAndColors.GetColor(_initialColor);
        _laserRenderer.LineRenderer.endColor = LayersAndColors.GetColor(_initialColor);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastTarget, 15f, LayersAndColors.LightLayerMask);
        _laserRenderer.LineRenderer.SetPosition(0, transform.position);
        _laserRenderer.LineRenderer.SetPosition(1, hit.collider is null ? transform.position + (Vector3)(_raycastTarget * 100f) : hit.point);
        if (hit.collider == null) return;
        GameObject objectHit = hit.collider.gameObject;
        if (objectHit == (_nextReflectable?.gameObject ?? null)) return;
        _nextReflectable?.StopReflection();
        _nextReflectable = objectHit.GetComponent<Reflectable>();
        _nextReflectable?.StartReflection(_raycastTarget, _initialColor, hit);
    }

    [Button]
    public void ApplyParameters(bool init = true)
    {
        if (_sprites.Count < (int)_initialColor && _sprites[(int)_initialColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)_initialColor];
        }
        else
        {
            _spriteRenderer.color = LayersAndColors.GetColor(_initialColor);
        }
        if (init)
        {
            FindObjectsOfType<StartingLaser>().Where(x => x != this).ToList().ForEach(x => x.ApplyParameters(false));
        }
    }
}