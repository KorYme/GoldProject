using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Reflectable
{
    [Header("Mirror Parameters")]
    [SerializeField, OnValueChanged(nameof(FlipMirror))] bool _isFlipped;

    protected override void Awake()
    {
        base.Awake();
        FlipMirror();
    }

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        Debug.Log("LaserColor = " + laserColor + " OutputColor " + _outputLaserColor);
        _inputLaserColor = laserColor;
        LaserOrigin = transform.position;
        if (Vector2.Dot(laserDirection, transform.up) > 0)
            LaserDirection = Vector2.Reflect(laserDirection, transform.up * -1);
        else
            LaserDirection = Vector2.Reflect(laserDirection, transform.up);
        base.StartReflection(LaserDirection, laserColor, raycast, previous);
    }

    [Button("Update Mirror")]
    private void FlipMirror()
    {
        transform.parent.rotation = Quaternion.Euler(0f, _isFlipped ? 180f : 0f, 0f);
    }
}
