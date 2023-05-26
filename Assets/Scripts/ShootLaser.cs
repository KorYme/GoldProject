using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    bool _shouldShoot = false;

    PlayerHitByRay _tempPlayer;
    PlayerHitByRay _currentPlayer;
    
    public bool ShouldShoot
    {
        get { return _shouldShoot; }
        set { _shouldShoot = value; }
    }

    private void Update()
    {
       
    }
}
