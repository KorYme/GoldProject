using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalLaser : Reflectable
{

    [Space(5), Header("Final Target")] 
    [SerializeField] Utilities.GAMECOLORS _targetColor;
    [SerializeField] Utilities.DIRECTIONS _sideTouchedNeeded;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

    float _timeHitByLaser = 0;
    bool _isLevelComplete;

    const float ANGLE_TOLERANCE = 3f;

    protected override void Awake()
    {
        _onReflection = null;
        _isLevelComplete = false;
    }

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        if (_isLevelComplete) return;
        _inputLaserColor = laserColor;
        LaserDirection = laserDirection;
        if (Vector3.Angle(laserDirection, -Utilities.GetDirection(_sideTouchedNeeded)) > ANGLE_TOLERANCE)
        {
            if (_onReflection == null) return;
            StopReflection();
        }
        else if (laserColor == _targetColor && _onReflection == null)
        {
            _onReflection += ReflectLaser;
        }
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
            _isLevelComplete = true;
            LevelCompleted();
            StopReflection();
        }
        else
        {
            _timeHitByLaser += Time.deltaTime;
        }
    }

    private void LevelCompleted()
    {
        _particleSystem?.Play();
        Debug.Log("Level fini");
    }

    [Button]
    public void ApplyParameters(bool init = true)
    {
        if (_sprites.Count > (int)_targetColor && _sprites[(int)_targetColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)_targetColor];
        }
        else
        {
            _spriteRenderer.color = Utilities.GetColor(_targetColor);
        }   
        if (init)
        {
            FindObjectsOfType<LensFilter>().Where(x => x != this).ToList().ForEach(x => x.ApplyParameters(false));
        }
    }
}
