using KorYmeLibrary.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IDataSaveable<GameData>
{
    SerializableDictionnary<int, int> _levelDictionnary;

    int _totalStarNumber;
    public int TotalStarNumber
    {
        get
        {
            return _totalStarNumber;
        }
        private set
        {
            if (_totalStarNumber == value) return;
            _totalStarNumber = value;
        }
    }

    SerializableDictionnary<int, SKINSTATE> _skinDictionnary;

    public static DataManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already one DataManager in the scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CompleteALevel(int levelID, int starsNumber)
    {
        if (starsNumber <= 0) return;
        TotalStarNumber += starsNumber - (_levelDictionnary.ContainsKey(levelID) ? _levelDictionnary[levelID] : 0);
        _levelDictionnary[levelID] = starsNumber;
    }

    public void LoadData(GameData gameData)
    {
        _levelDictionnary = gameData.LevelDictionnary;
        TotalStarNumber = gameData.StarNumber;
        _skinDictionnary = gameData.SkinDictionnary;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.LevelDictionnary = _levelDictionnary;
        gameData.StarNumber = TotalStarNumber + 1;
        gameData.SkinDictionnary = _skinDictionnary;
    }

    public void InitializeData()
    {
        TotalStarNumber = 0;
        _levelDictionnary = new();
        _skinDictionnary = new();
    }
}
