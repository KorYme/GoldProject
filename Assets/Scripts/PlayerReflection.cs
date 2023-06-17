using NaughtyAttributes;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReflection : Reflectable
{
    bool _shouldPlayStartReflectSound = true;
    
    [SerializeField] protected Transform _crystalTransform;
    [SerializeField] protected PlayerController _playerController;
    [SerializeField] AnimatorManager _animatorManager;

    [Space(10f)]
    [SerializeField, Foldout("Events")] UnityEvent _onReflect;

    public override Vector2 LaserOrigin { get => _crystalTransform.position; }
    public override Vector2 LaserDirection { get => _crystalTransform.position - transform.position; }

    protected override Utilities.GAMECOLORS _outputLaserColor {
        get
        {
            return Utilities.GetSubtractedColor(base._outputLaserColor, LensColor);
        }
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
        ForbiddenAngle = -4000;
        LensColor = Utilities.GAMECOLORS.White;
        base.Awake();
    }

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        if (_shouldPlayStartReflectSound)
        {
            AudioManager.Instance.NbOfPlayersReflecting++;
            AudioManager.Instance.PlayPlayerReflectSound();
            _shouldPlayStartReflectSound = false;
        }
        base.StartReflection(laserDirection, laserColor, raycast, previous);
        if (previous == _previousReflectable)
        {
            ForbiddenAngle = ((int)(Mathf.Atan2(-laserDirection.y, -laserDirection.x) * Mathf.Rad2Deg) + 360) % 360;
        }
        if (!_playerController.IsMoving && !_playerController.IsHittingWall && !_playerController.IsRefusing)
        {
            _animatorManager.ChangeAnimation(IsReflecting ? ANIMATION_STATES.Reflection : ANIMATION_STATES.Idle);
        }
        _playerController?.CheckNeededRotation();
    }

    public override void StopReflection()
    {
        base.StopReflection();
        ForbiddenAngle = -4000;
        if (!_playerController.IsMoving)
        {
            _animatorManager.ChangeAnimation(ANIMATION_STATES.Idle);
        }
        AudioManager.Instance.NbOfPlayersReflecting--;
        _shouldPlayStartReflectSound = true;
    }
}
