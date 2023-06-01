using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LensFilter : Reflectable
{
    [Header("Lensfilter Parameters")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;

    [Header("Events")]
    [SerializeField] UnityEvent _onFilteringStart;
    [SerializeField] UnityEvent _onFilteringStop;

    public override Vector2 LaserOrigin { get => transform.position; }
    PlayerReflection _lastPlayerMet;

    public override void StartReflection(Vector2 laserDirection, LayersAndColors.GAMECOLORS laserColor, RaycastHit2D raycast)
    {
        _onFilteringStart.Invoke();
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
