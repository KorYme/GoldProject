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
            #if UNITY_EDITOR
            ChangeParameters();
            #endif
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
            FindObjectsOfType<TileBehaviour>().Where(x => x != this).ToList().ForEach(x => x.ChangeParameters(false));
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
            case TileType.FilterLens:
            case TileType.Crate:
            case TileType.Mud:
                _collider.enabled = false;
                _spriteRenderer.color = Color.white;
                gameObject.layer = LayerMask.NameToLayer("Default");
                break;
            case TileType.ColoredWall:
            case TileType.LaserStart:
            case TileType.LaserEnd:
            case TileType.Glass:
            case TileType.Mirror:
                _collider.enabled = false;
                _spriteRenderer.color = Color.clear;
                gameObject.layer = LayerMask.NameToLayer("Default");
                break;
            default:
                break;
        }
        ApplyForAll(init);
    }

    private void ApplyForAll(bool init)
    {
        if (init)
        {
            FindObjectsOfType<MonoBehaviour>().OfType<IUpdateableTile>().ToList().ForEach(x => x.UpdateTile(false));
        }
        if (_prefabs.Count <= (int)Type) return;
        if (_prefabs[(int)Type] != null)
        {
            if (transform.childCount == 1)
            {
                if (transform.GetChild(0).name == _prefabs[(int)Type].name) return;
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            else if (transform.childCount >= 2)
            {
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }
            _currentTile = UnityEditor.PrefabUtility.InstantiatePrefab(_prefabs[(int)Type]) as GameObject;
            _currentTile.transform.SetParent(transform, false);
        }
        else
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            _currentTile = null;
        }
    }
    #endif
}
