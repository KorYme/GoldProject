using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Reflectable
{
    [SerializeField] bool _isFlipped = false;
    Vector2 _normalToCheck;

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        _inputLaserColor = laserColor;
        LaserOrigin = transform.position;
        if (_isFlipped)
            _normalToCheck = transform.up * -1;
        else
            _normalToCheck = transform.up;


        if (Vector2.Dot(laserDirection, _normalToCheck) > 0)
            LaserDirection = Vector2.Reflect(laserDirection, _normalToCheck * -1);
        else
            LaserDirection = Vector2.Reflect(laserDirection, _normalToCheck);

        base.StartReflection(LaserDirection, laserColor, raycast);
    }
}
