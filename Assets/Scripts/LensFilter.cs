using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LensFilter : Reflectable
{
    public override Vector2 LaserOrigin { get => transform.position; }

    PlayerReflection _lastPlayerMet;

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        LaserDirection = laserDirection;
        base.StartReflection(LaserDirection, laserColor, raycast);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _lastPlayerMet = collision.GetComponent<PlayerReflection>();
            _lastPlayerMet.LensColor = _reflectionColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _lastPlayerMet.LensColor = LayersAndColors.GAMECOLORS.White;
            _lastPlayerMet = null;
        }
    }
}
