using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FinalTarget : Reflectable
{

    [Space(5), Header("Final Target")] 
    [SerializeField] LayersAndColors.GAMECOLORS _targetColor;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

    [Header("Events")]
    [SerializeField] UnityEvent _onLaserStart;
    [SerializeField] UnityEvent _onLaserStop;
    [SerializeField] UnityEvent _onLevelComplete;

    float _timeHitByLaser = 0;
    bool _isLevelComplete;

    protected override void Awake()
    {
        _onReflection = null;
        _isLevelComplete = false;
    }

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        if (_isLevelComplete) return;
        _inputLaserColor = laserColor;
        if (laserColor == _targetColor && _onReflection == null)
        {
            _onReflection += ReflectLaser;
            _onLaserStart.Invoke();
        }
    }

    public override void StopReflection()
    {
        _timeHitByLaser = 0;
        _onReflection -= ReflectLaser;
        _onLaserStop.Invoke();
    }

    protected override void ReflectLaser()
    {
        if (_timeHitByLaser >= 2f)
        {
            _onLevelComplete.Invoke();
            _isLevelComplete = true;
            LevelCompleted();
            StopReflection();
        }
        else
            _timeHitByLaser += Time.deltaTime;
    }

    private void LevelCompleted()
    {
        _particleSystem?.Play();
        Debug.Log("Level fini");
    }

    [Button]
    public void ApplyParameters(bool init = true)
    {
        if (_sprites.Count < (int)_reflectionColor && _sprites[(int)_reflectionColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)_reflectionColor];
        }
        else
        {
            _spriteRenderer.color = LayersAndColors.GetColor(_reflectionColor);
        }
        if (init)
        {
            FindObjectsOfType<LensFilter>().Where(x => x != this).ToList().ForEach(x => x.ApplyParameters(false));
        }
    }
}
