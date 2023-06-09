using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("References")]
    [SerializeField] Transform _tilesContainer;
    [SerializeField] Transform _bordersContainer;
    [SerializeField] GameObject _tilePrefab;

    [Header("Parameters")]
    [SerializeField, MinMaxSlider(1,16)] Vector2Int _mapSize;

    private void Reset()
    {
        _mapSize = new Vector2Int(6, 8);
    }

    [Button]
    private void GenerateLevel()
    {
        for (int x = 0; x < _mapSize.x + 2; x++)
        {
            for (int y = 0; y < _mapSize.y + 2; y++)
            {
                GameObject newTile = UnityEditor.PrefabUtility.InstantiatePrefab(_tilePrefab) as GameObject;
                if (x == 0 || y == 0 || x == _mapSize.x + 1 || y == _mapSize.y + 1)
                {
                    newTile.transform.position = new Vector3(x - (_mapSize.x + 1) * .5f, y - (_mapSize.y + 1) * .5f, 0);
                    newTile.transform.SetParent(_bordersContainer, false);
                    newTile.GetComponent<TileBehaviour>().Type = TileBehaviour.TileType.Border;
                    newTile.name = $"Border ({x-1},{y-1})";
                }
                else
                {
                    newTile.transform.position = new Vector3(x - (_mapSize.x + 1) * .5f, y - (_mapSize.y + 1) * .5f, 0);
                    newTile.transform.SetParent(_tilesContainer, false);
                    newTile.GetComponent<TileBehaviour>().Type = TileBehaviour.TileType.Empty;
                    newTile.name = $"Tile ({x-1},{y-1})";
                }
            }
        }
    }

    [Button]
    private void DestroyOldLevel()
    {
        for (int i = _tilesContainer.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_tilesContainer.GetChild(i).gameObject);
        }
        for (int y = _bordersContainer.childCount - 1; y >= 0; y--)
        {
            DestroyImmediate(_bordersContainer.GetChild(y).gameObject);
        }
    }
#endif
}
