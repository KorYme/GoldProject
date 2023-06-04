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
    [SerializeField] List<Sprite> _sprites;

    [Header("Events")]
    [SerializeField] UnityEvent _onFilteringStart;
    [SerializeField] UnityEvent _onFilteringStop;

    public override Vector2 LaserOrigin { get => transform.position; }
    PlayerReflection _lastPlayerMet;

    public override void StartReflection(Vector2 laserDirection, Utilities.GAMECOLORS laserColor, RaycastHit2D raycast, Reflectable previous)
    {
        _onFilteringStart.Invoke();
        LaserDirection = laserDirection;
        base.StartReflection(LaserDirection, laserColor, raycast, previous);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _lastPlayerMet = collision.GetComponent<PlayerReflection>();
            _lastPlayerMet.LensColor = _reflectionColor;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("OnlyLight");
            _lastPlayerMet.LensColor = Utilities.GAMECOLORS.White;
            _lastPlayerMet = null;
        }
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
