using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FinalLaser : Reflectable, IUpdateableTile
{
    [Header("Events")]
    [SerializeField] UnityEvent _onLaserRampUp;
    [SerializeField] UnityEvent _onLaserStop;

    [Space(5), Header("Final Target References")] 
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

    [Header("Final Target Parameters")]
    [SerializeField, OnValueChanged(nameof(ApplyChange))] Utilities.GAMECOLORS _targetColor;
    [SerializeField, Tooltip("Not needed anymore")] Utilities.DIRECTIONS _sideTouchedNeeded;
    [SerializeField] WinMenuManager _winMenuManager;

    float _timeHitByLaser = 0;
    bool _isLevelComplete;
    bool _shouldPlayWrongColorSound = true;
    ParticleSystem _pSystem;

    const float ANGLE_TOLERANCE = 3f;

    private void Start()
    {
        _pSystem = Instantiate(_particleSystem);
        _pSystem.Stop();
        _pSystem.transform.position = transform.position;// :) coucou Maxime
    }

    protected override void Awake()
    {
        _onReflection = null;
        _isLevelComplete = false;
        UpdateTile(false);
    }

    private void Reset()
    {
        _winMenuManager = FindObjectOfType<WinMenuManager>();
    }

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        if (_isLevelComplete) return;
        _onLaserRampUp.Invoke();
        _inputLaserColor = laserColor;
        LaserDirection = laserDirection;

        // Direction checking
        //if (Vector3.Angle(laserDirection, -Utilities.GetDirection(_sideTouchedNeeded)) > ANGLE_TOLERANCE)
        //{
        //    if (_onReflection == null) return;
        //    StopReflection();
        //    return;
        //}
        if ( _onReflection == null)
        {
            
            
            if (laserColor == _targetColor)
            {
                var main = _pSystem.main;
                main.startColor = Utilities.GetColor(laserColor);
                if (!_pSystem.isPlaying)
                {
                    _pSystem.Play();
                    AudioManager.Instance.PlaySound("ReceiveRightColor");
                    _onReflection += ReflectLaser;
                }
            }
            else
            {
                if (_shouldPlayWrongColorSound)
                {
                    AudioManager.Instance.PlaySound("ReceiveWrongColor");
                    _shouldPlayWrongColorSound = false;
                }
            }
        }
    }

    public override void StopReflection()
    {
        _onLaserStop.Invoke();
        _timeHitByLaser = 0;
        _onReflection -= ReflectLaser;
        _pSystem.Stop();
        AudioManager.Instance.StopSound("ReceiveRightColor");
        AudioManager.Instance.PlaySound("ReceiveWrongColor");
        _shouldPlayWrongColorSound = true;
    }

    protected override void ReflectLaser()
    {
        if (_timeHitByLaser >= 2f && InputManager.Instance.CanMoveAPlayer)
        {
            _isLevelComplete = true;
            LevelCompleted();
        }
        else
        {
            _timeHitByLaser += Time.deltaTime;
        }
    }

    private void LevelCompleted()
    {

        _winMenuManager.Win(InputManager.Instance.MovementNumber);
    }



    [Button]
    public void UpdateTile(bool init = true)
    {
        _winMenuManager = FindObjectOfType<WinMenuManager>();
        if (_sprites.Count > (int)_targetColor && _sprites[(int)_targetColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)_targetColor];
            _spriteRenderer.color = Color.white;
        }
        else
        {
            _spriteRenderer.color = Utilities.GetColor(_targetColor);
        }   
        if (init)
        {
            FindObjectsOfType<MonoBehaviour>().Where(x => x != this).OfType<IUpdateableTile>().ToList().ForEach(x => x.UpdateTile(false));
        }
    }

    private void ApplyChange()
    {
        UpdateTile(true);
    }
}
