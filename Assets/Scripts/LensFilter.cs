using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LensFilter : Reflectable, IUpdateableTile
{
    [Header("Lensfilter Parameters")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;
    [SerializeField] List<Sprite> _sprites;

    [Header("Events")]
    [SerializeField] UnityEvent _onFilteringStart;
    [SerializeField] UnityEvent _onFilteringStop;

    public override Vector2 LaserOrigin { get => transform.position; }
    PlayerReflection _lastPlayerMet;
    Coroutine _idleSpreadAnimationCoroutine;
    Coroutine _idleAnimationCoroutine;
    bool _shouldPlayReflectSound = false;

    protected override void Awake()
    {
        base.Awake();
        _animator.Play("Lens_Idle_" + _reflectionColor.ToString());
    }

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        if (_shouldPlayReflectSound)
        {
            AudioManager.Instance.PlaySound("LensFilter");
            _shouldPlayReflectSound = false;
        }
        _onFilteringStart.Invoke();
        LaserDirection = laserDirection;
        base.StartReflection(LaserDirection, laserColor, raycast, previous);
    }

    public override void StopReflection()
    {
        base.StopReflection();
        _shouldPlayReflectSound = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_idleAnimationCoroutine != null)
            {
                StopCoroutine(_idleAnimationCoroutine);
            }
            _animator.Play("Lens_Spread_" + _reflectionColor.ToString());
            _idleSpreadAnimationCoroutine = StartCoroutine(IdleSpreadAnimation(
                _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == "Lens_Spread_" + _reflectionColor.ToString()).length));
            _lastPlayerMet = collision.GetComponent<PlayerReflection>();
            _lastPlayerMet.LensColor = _reflectionColor;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_idleSpreadAnimationCoroutine != null)
            {
                StopCoroutine(_idleSpreadAnimationCoroutine);
            }
            _animator.Play("Lens_Unspread_" + _reflectionColor.ToString());
            _idleAnimationCoroutine = StartCoroutine(IdleAnimation(
                _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == "Lens_Unspread_" + _reflectionColor.ToString()).length));
            gameObject.layer = LayerMask.NameToLayer("OnlyLight");
            _lastPlayerMet.LensColor = Utilities.GAMECOLORS.White;
            _lastPlayerMet = null;
        }
    }

    IEnumerator IdleSpreadAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        _animator.Play("Lens_Spread_Idle_" + _reflectionColor.ToString());
    }

    IEnumerator IdleAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        _animator.Play("Lens_Idle_" + _reflectionColor.ToString());
    }

    [Button]
    public void UpdateTile(bool init = true)
    {
        if (_sprites.Count > (int)_reflectionColor && _sprites[(int)_reflectionColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)_reflectionColor];
        }
        else
        {
            _spriteRenderer.color = Utilities.GetColor(_reflectionColor);
        }
        if (init)
        {
            FindObjectsOfType<MonoBehaviour>().Where(x => x != this).OfType<IUpdateableTile>().ToList().ForEach(x => x.UpdateTile(false));
        }
    }
}
