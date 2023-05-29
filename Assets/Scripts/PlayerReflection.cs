using UnityEngine;

public class PlayerReflection : Reflectable
{
    [SerializeField] protected Transform _crystalTransform;
    [SerializeField] protected PlayerController _playerController;

    public override Vector2 LaserOrigin { get => _crystalTransform.position; }
    public override Vector2 LaserDirection { get => _crystalTransform.position - transform.position; }

    public int ForbiddenAngle
    {
        get; private set;
    }

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        base.StartReflection(laserDirection, laserColor, raycast);
        ForbiddenAngle = ((int)((Mathf.Atan2(-laserDirection.y, -laserDirection.x) * Mathf.Rad2Deg) + Mathf.Epsilon) + 360) % 360;
        _playerController.RotatePlayer(laserDirection);
    }

    public override void StopReflection()
    {
        base.StopReflection();
        ForbiddenAngle = -10;
    }
}
