using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTarget : Reflectable
{
    [SerializeField] LayersAndColors.GAMECOLORS _targetColor;
    float _timeHitByLaser = 0;


    protected override void Awake()
    {
        _onReflection = null;
    }

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        _inputLaserColor = laserColor;
        if (laserColor == _targetColor)
            _onReflection += ReflectLaser;
    }

    public override void StopReflection()
    {
        _timeHitByLaser = 0;
        _onReflection -= ReflectLaser;
    }

    protected override void ReflectLaser()
    {
        if (_timeHitByLaser >= 2f)
        {
            LevelCompleted();
        }
        else
            _timeHitByLaser += Time.deltaTime;
    }

    private void LevelCompleted()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
