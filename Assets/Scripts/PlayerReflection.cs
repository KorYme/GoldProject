using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReflection : Reflectable
{
    [SerializeField] protected Transform _crystalTransform;
    [SerializeField] protected PlayerController _playerController;

    [Space(10f)]
    [SerializeField, Foldout("Events")] UnityEvent _onReflect;

    public override Vector2 LaserOrigin { get => _crystalTransform.position; }
    public override Vector2 LaserDirection { get => _crystalTransform.position - transform.position; }

    protected override Utilities.GAMECOLORS _outputLaserColor {
        get => Utilities.GetSubtractedColor(base._outputLaserColor, LensColor);
    }

    Utilities.GAMECOLORS _lensColor;
    public Utilities.GAMECOLORS LensColor 
    {
        get => _lensColor;
        set
        {
            _lensColor = value;
            UpdateColorLaser();
        }
    }

    public int ForbiddenAngle
    {
        get; set;
    }

    protected override void Awake()
    {
        LensColor = Utilities.GAMECOLORS.White;
        base.Awake();
    }

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        base.StartReflection(laserDirection, laserColor, raycast, previous);
        if (previous == _previousReflectable)
        {
            ForbiddenAngle = ((int)(Mathf.Atan2(-laserDirection.y, -laserDirection.x) * Mathf.Rad2Deg) + 360) % 360;
        }
        _playerController?.CheckNeededRotation();
    }

    public override void StopReflection()
    {
        base.StopReflection();
        ForbiddenAngle = -4000;
    }
}
