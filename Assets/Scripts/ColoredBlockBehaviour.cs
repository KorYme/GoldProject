using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColoredBlockBehaviour : MonoBehaviour, IUpdateableTile
{
    [Header("References")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField] Utilities.GAMECOLORS _blockColor;

    public Utilities.GAMECOLORS BlockColor
    {
        get => _blockColor;
        set
        {
            _blockColor = value;
        }
    }

    private void Reset()
    {
        UpdateTile();
    }

    [Button]
    public void UpdateTile(bool init = true)
    {
        _spriteRenderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer(BlockColor.ToString() + "Wall");
        if (_sprites.Count > (int)BlockColor && _sprites[(int)BlockColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)BlockColor];
        }
        else
        {
            _spriteRenderer.color = Utilities.GetColor(BlockColor);
        }
        if (init)
        {
            FindObjectsOfType<MonoBehaviour>().Where(x => x != this).OfType<IUpdateableTile>().ToList().ForEach(x => x.UpdateTile(false));
        }
    }
}
