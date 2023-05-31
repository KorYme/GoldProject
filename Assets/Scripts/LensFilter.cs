using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensFilter : Reflectable
{
    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        LaserOrigin = transform.position;
        LaserDirection = laserDirection;
        base.StartReflection(LaserDirection, laserColor, raycast);
    }
}
