using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColoredBlockBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField] LayersAndColors.GAMECOLORS _blockColor;

    public LayersAndColors.GAMECOLORS BlockColor
    {
        get => _blockColor;
        set
        {
            _blockColor = value;
        }
    }

    private void Reset()
    {
        ApplyParameters();
    }

    [Button]
    public void ApplyParameters(bool init = true)
    {
        gameObject.layer = LayerMask.NameToLayer(BlockColor.ToString() + "Wall");
        if (_sprites.Count < (int)BlockColor && _sprites[(int)BlockColor] != null)
        {
            _spriteRenderer.sprite = _sprites[(int)BlockColor];
        }
        else
        {
            _spriteRenderer.color = LayersAndColors.GetColor(BlockColor);
        }
        if (init)
        {
            FindObjectsOfType<LensFilter>().Where(x => x != this).ToList().ForEach(x => x.ApplyParameters(false));
        }
    }
}
