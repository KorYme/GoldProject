using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        Border,
        BasicWall,
        BlueWall,
        RedWall,
        YellowWall,
        PurpleWall,
        GreenWall,
        OrangeWall,
        Mud,
    }

    [Header("References")]
    [SerializeField] BoxCollider2D _collider;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Parameters")]
    [OnValueChanged(nameof(ChangeParameters))]
    [SerializeField] TileType _type;

    public TileType Type
    {
        get => _type;
        set 
        { 
            _type = value;
            ChangeParameters();
        }
    }

    private void Reset()
    {
        Type = TileType.Border;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void ChangeParameters()
    {
        switch (_type)
        {
            case TileType.Empty:
                gameObject.layer = LayerMask.NameToLayer("Default");
                _collider.enabled = false;
                _spriteRenderer.color = Color.clear;
                break;
            case TileType.Border:
                _collider.enabled = true;
                _spriteRenderer.color = Color.grey;
                gameObject.layer = LayerMask.NameToLayer("BasicWall");
                break;
            case TileType.Mud:
                _collider.enabled = true;
                _spriteRenderer.color = Color.grey;
                gameObject.layer = LayerMask.NameToLayer("Mud");
                break;
            case TileType.BasicWall:
            case TileType.BlueWall:
            case TileType.RedWall:
            case TileType.YellowWall:
            case TileType.PurpleWall:
            case TileType.GreenWall:
            case TileType.OrangeWall:
                _collider.enabled = true;
                gameObject.layer = LayerMask.NameToLayer(_type.ToString());
                _spriteRenderer.color = ChangeColor();
                break;
            default:
                break;
        }
    }

    private Color ChangeColor()
    {
        switch (_type)
        {
            case TileType.BasicWall:
                return Color.grey;
            case TileType.BlueWall:
                return Color.blue;
            case TileType.RedWall:
                return Color.red;
            case TileType.YellowWall:
                return Color.yellow;
            case TileType.PurpleWall:
                return new Color(0.8f, 0.0f, 0.8f);
            case TileType.GreenWall:
                return Color.green;
            case TileType.OrangeWall:
                return new Color(1.0f, 0.64f, 0.0f);
            default:
                return Color.grey;
        }
    }
}
