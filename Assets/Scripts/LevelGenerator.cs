using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera _camera;
    [SerializeField] Transform _tilesContainer;
    [SerializeField] Transform _player;
    [SerializeField] GameObject _tilePrefab;

    [Header("Parameters")]
    [SerializeField, MinMaxSlider(3,9)] Vector2Int _mapSize;
    [SerializeField, Range(0f, 2f)] float _offsetOnSide;


    [Header("TEST")]
    [SerializeField] LayersAndColors.GAMECOLORS _color1;
    [SerializeField] LayersAndColors.GAMECOLORS _color2;
    [SerializeField] LayerMask _testLayer;

    private void Reset()
    {
        _mapSize = new Vector2Int(6, 8);
        _offsetOnSide = 0;
    }

    [Button]
    private void GenerateLevel()
    {
        _camera.orthographicSize = _mapSize.x + _offsetOnSide * 2;
        _player.position = new Vector3(((_mapSize.y + 1) % 2) * .5f, ((_mapSize.y + 1) % 2) * .5f, 0);
        for (int x = 0; x < _mapSize.x + 2; x++)
        {
            for (int y = 0; y < _mapSize.y + 2; y++)
            {
                GameObject newTile = Instantiate(_tilePrefab, new Vector3(x - (_mapSize.x + 1) * .5f, y - (_mapSize.y + 1) * .5f, 0), 
                    Quaternion.identity, _tilesContainer);
                if (x == 0 || y == 0 || x == _mapSize.x + 1 || y == _mapSize.y + 1)
                {
                    newTile.GetComponent<TileBehaviour>().Type = TileBehaviour.TileType.Border;
                }
                else
                {
                    newTile.GetComponent<TileBehaviour>().Type = TileBehaviour.TileType.Empty;
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
    }

    [Button]
    private void ResizeCamera()
    {
        _camera.orthographicSize = _mapSize.x + _offsetOnSide * 2;
    }

    [Button]
    private void TestColorMix()
    {
        Debug.Log(LayersAndColors.GetSubtractedColor(_color1, _color2).ToString());
    }
    
    [Button]
    private void LayerTest()
    {
        _testLayer = LayerMask.GetMask("BasicWall", "Player");
    }
}
