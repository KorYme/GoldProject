using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Reflectable
{
    [Header("Mirror Parameters")]
    [SerializeField, OnValueChanged(nameof(FlipMirror))] bool _isFlipped;

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        _inputLaserColor = laserColor;
        LaserOrigin = transform.position;
        if (Vector2.Dot(laserDirection, transform.up) > 0)
            LaserDirection = Vector2.Reflect(laserDirection, transform.up * -1);
        else
            LaserDirection = Vector2.Reflect(laserDirection, transform.up);
        base.StartReflection(LaserDirection, laserColor, raycast);
    }

    private void FlipMirror()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, _isFlipped ? 45 : -45);
    }
}
