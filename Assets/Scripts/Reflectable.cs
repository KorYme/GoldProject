using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflectable : MonoBehaviour
{
    public enum ReflectionType
    {
        Additive,
        Substractive,
    }

    [Header("References")]
    [SerializeField] protected LineRenderer _lineRenderer;
    [SerializeField] protected LayersAndColors.GAMECOLORS _reflectionColor;
    [SerializeField] protected ReflectionType _reflectionType;

    protected Vector2 _laserDirection;
    public virtual Vector2 LaserDirection
    {
        get; set;
    }
    protected Vector2 _laserOrigin;
    public virtual Vector2 LaserOrigin
    {
        get; set;
    }
    protected Action _onReflection;
    protected Reflectable _nextReflectable;
    protected LayersAndColors.GAMECOLORS _inputLaserColor;
    protected virtual LayersAndColors.GAMECOLORS _outputLaserColor
    {
        get
        {
            switch (_reflectionType)
            {
                case ReflectionType.Additive:
                    return LayersAndColors.GetMixedColor(_inputLaserColor, _reflectionColor);
                case ReflectionType.Substractive:
                    return LayersAndColors.GetSubtractedColor(_inputLaserColor, _reflectionColor);
                default:
                    return _reflectionColor;
            }
        }
    }

    public bool IsReflecting
    {
        get => _onReflection != null;
    }

    private void Reset()
    {
        _reflectionColor = LayersAndColors.GAMECOLORS.White;
        _reflectionType = ReflectionType.Additive;
    }

    protected virtual void Awake()
    {
        _onReflection = null;
        _nextReflectable = null;
        _lineRenderer.enabled = false;
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
    }

    public virtual void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        if (IsReflecting) return;
        _inputLaserColor = laserColor;
        _lineRenderer.startColor = LayersAndColors.GetColor(_outputLaserColor);
        _lineRenderer.endColor = LayersAndColors.GetColor(_outputLaserColor);
        _lineRenderer.enabled = true;
        _onReflection += ReflectLaser;
    }

    public virtual void StopReflection()
    {
        if (!IsReflecting) return;
        _lineRenderer.enabled = false;
        _onReflection -= ReflectLaser;
        _nextReflectable?.StopReflection();
    }

    protected void Update()
    {
        _onReflection?.Invoke();
    }

    protected virtual void ReflectLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(LaserOrigin, LaserDirection, 15f, LayersAndColors.LightLayerMask);
        _lineRenderer.SetPosition(0, LaserOrigin);
        _lineRenderer.SetPosition(1, hit.collider is null ? transform.position + (Vector3)(LaserDirection * 100f) : hit.point);
        if (hit.collider == null) return;
        GameObject objectHit = hit.collider.gameObject;
        if (objectHit == (_nextReflectable?.gameObject ?? null)) return;
        _nextReflectable?.StopReflection();
        _nextReflectable = objectHit.GetComponent<Reflectable>();
        if ((_nextReflectable?.IsReflecting) ?? true)
        {
            _nextReflectable = null;
        }
        else
        {
            _nextReflectable?.StartReflection(LaserDirection, _outputLaserColor, hit);
        }
    }
}