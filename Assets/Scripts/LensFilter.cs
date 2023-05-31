using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensFilter : Reflectable
{
    public override Vector2 LaserOrigin { get => transform.position; }

    PlayerReflection _lastPlayerMet;

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        LaserDirection = laserDirection;
        base.StartReflection(LaserDirection, laserColor, raycast);
    }
}
