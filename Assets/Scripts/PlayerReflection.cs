using UnityEngine;
using UnityEngine.Events;

public class PlayerReflection : Reflectable
{
    [SerializeField] protected Transform _crystalTransform;
    [SerializeField] protected PlayerController _playerController;

    [Header("Events")]
    [SerializeField] UnityEvent _onReflectionStarted;

    public override Vector2 LaserOrigin { get => _crystalTransform.position; }
    public override Vector2 LaserDirection { get => _crystalTransform.position - transform.position; }

    protected override LayersAndColors.GAMECOLORS _outputLaserColor {
        get => LayersAndColors.GetSubtractedColor(base._outputLaserColor, LensColor);
    }

    LayersAndColors.GAMECOLORS _lensColor;
    public LayersAndColors.GAMECOLORS LensColor 
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
        get; private set;
    }

    protected override void Awake()
    {
        LensColor = LayersAndColors.GAMECOLORS.White;
        base.Awake();
    }

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        _onReflectionStarted.Invoke();
        ForbiddenAngle = ((int)((Mathf.Atan2(-laserDirection.y, -laserDirection.x) * Mathf.Rad2Deg) + Mathf.Epsilon) + 360) % 360;
        if (IsReflecting) return;
        base.StartReflection(laserDirection, laserColor, raycast);
        _playerController.RotatePlayer(laserDirection);
    }

    public override void StopReflection()
    {
        base.StopReflection();
        ForbiddenAngle = -10;
    }
}
