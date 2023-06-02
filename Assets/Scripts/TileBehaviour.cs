using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileBehaviour : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        Border,
        ColoredWall,
        LaserStart,
        LaserEnd,
        Glass,
        Mirror,
        FilterLens,
        Mud,
        Crate,
    }

    [Header("References")]
    [SerializeField] BoxCollider2D _collider;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<GameObject> _prefabs;

    [Header("Current Object")]
    [SerializeField , Tooltip("Do not touch this value, it is only useful to find the prefab instantiated here")] GameObject _currentTile;

    [Header("Parameters")]
    [SerializeField] TileType _type;

    public TileType Type
    {
        get => _type;
        set
        {
            if (_type == value) return;
            _type = value;
            ChangeParameters();
        }
    }

    private void OnDestroy()
    {
        if (_currentTile == null) return;
        DestroyImmediate(_currentTile);
    }

    #if UNITY_EDITOR
    [Button("Apply Parameters")]
    public void ChangeParameters(bool init = true)
    {
        if (init)
        {
            if (_currentTile != null)
            {
                DestroyImmediate(_currentTile);
                _currentTile = null;
            }
            FindObjectsOfType<TileBehaviour>().Where(x => x != this && _currentTile != null && Type == TileType.ColoredWall).ToList().ForEach(x => x.ChangeParameters(false));
        }
        switch (Type)
        {
            case TileType.Empty:
                _collider.enabled = false;
                _spriteRenderer.color = Color.white;
                gameObject.layer = LayerMask.NameToLayer("Default");
                break;
            case TileType.Border:
                _collider.enabled = true;
                _spriteRenderer.color = Color.grey;
                gameObject.layer = LayerMask.NameToLayer("Border");
                break;
            case TileType.ColoredWall:
            case TileType.LaserStart:
            case TileType.LaserEnd:
            case TileType.Glass:
            case TileType.Mirror:
            case TileType.FilterLens:
            case TileType.Mud:
            case TileType.Crate:
                _collider.enabled = false;
                _spriteRenderer.color = Color.clear;
                gameObject.layer = LayerMask.NameToLayer("Default");
                if (_prefabs.Count <= (int)Type) return;
                if (_prefabs[(int)Type] != null)
                {
                    _currentTile = UnityEditor.PrefabUtility.InstantiatePrefab(_prefabs[(int)Type]) as GameObject;
                    _currentTile.transform.SetParent(transform, false);
                }
                else
                {
                    _currentTile = null;
                }
                break;
            default:
                break;
        }
    }
    #endif
}
