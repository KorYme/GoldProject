using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Reflectable
{
    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        LaserOrigin = transform.position;
        LaserDirection = Vector2.Reflect(laserDirection, raycast.normal);
        base.StartReflection(laserDirection, laserColor, raycast);
    }
}
