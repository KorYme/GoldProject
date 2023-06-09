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
    [SerializeField] protected LaserRenderer _laserRenderer;
    [SerializeField] protected Utilities.GAMECOLORS _reflectionColor;
    [SerializeField] protected ReflectionType _reflectionType;

    public Utilities.GAMECOLORS ReflectionColor
    {
        get => _reflectionColor;
    }
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
    protected Reflectable _previousReflectable;
    protected Utilities.GAMECOLORS _inputLaserColor;
    protected virtual Utilities.GAMECOLORS _outputLaserColor
    {
        get
        {
            switch (_reflectionType)
            {
                case ReflectionType.Additive:
                    return Utilities.GetMixedColor(_inputLaserColor, _reflectionColor);
                case ReflectionType.Substractive:
                    return Utilities.GetSubtractedColor(_inputLaserColor, _reflectionColor);
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
        _reflectionColor = Utilities.GAMECOLORS.White;
        _reflectionType = ReflectionType.Additive;
    }

    protected virtual void Awake()
    {
        _onReflection = null;
        _nextReflectable = null;
        if (_laserRenderer == null) return;
        _laserRenderer.LineRenderer.enabled = false;
        _laserRenderer.LineRenderer.useWorldSpace = true;
    }

    public virtual void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        if (IsReflecting || !enabled) return;
        _previousReflectable = previous;
        _inputLaserColor = laserColor;
        UpdateColorLaser();
        _laserRenderer.LineRenderer.enabled = true;
        _onReflection += ReflectLaser;
    }

    protected virtual void UpdateColorLaser()
    {
        _laserRenderer.ChangeLaserColor(_outputLaserColor);
    }

    public virtual void StopReflection()
    {
        _previousReflectable = null;
        if (!IsReflecting) return;
        _laserRenderer.LineRenderer.enabled = false;
        _onReflection -= ReflectLaser;
        _nextReflectable?.StopReflection();
        _nextReflectable = null;
    }

    protected void Update()
    {
        _onReflection?.Invoke();
    }

    protected virtual void ReflectLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(LaserOrigin, LaserDirection, 15f, Utilities.LightLayerMask);
        _laserRenderer.LineRenderer.SetPosition(0, LaserOrigin);
        _laserRenderer.LineRenderer.SetPosition(1, hit.collider != null ? hit.point : LaserOrigin);
        if (hit.collider == null) return;
        GameObject objectHit = hit.collider.gameObject;
        if (objectHit == (_nextReflectable?.gameObject ?? null))
        {
            if (_nextReflectable != null || _nextReflectable is Mirror)
            {
                _nextReflectable.StartReflection(LaserDirection, _outputLaserColor, hit, this);
            }
            return;
        }
        _nextReflectable?.StopReflection();
        _nextReflectable = objectHit.GetComponent<Reflectable>();
        if ((_nextReflectable?.IsReflecting) ?? true)
        {
            _nextReflectable = null;
        }
        else
        {
            _nextReflectable?.StartReflection(LaserDirection, _outputLaserColor, hit, this);
        }
    }
}