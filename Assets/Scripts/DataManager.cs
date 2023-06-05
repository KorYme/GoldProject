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

    float _volume;


    public static DataManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already one DataManager in the scene");
            transform.parent = null;
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
        TotalStarNumber = gameData.TotalStarNumber;
        _skinDictionnary = gameData.SkinDictionnary;
        _volume = gameData.Volume;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.LevelDictionnary = _levelDictionnary;
        gameData.TotalStarNumber = TotalStarNumber;
        gameData.SkinDictionnary = _skinDictionnary;
        gameData.Volume = _volume;
    }

    public void InitializeData()
    {
        TotalStarNumber = 0;
        _levelDictionnary = new();
        _skinDictionnary = new SerializableDictionnary<int, SKINSTATE>()
        {
            { 1 , SKINSTATE.ACQUIRED },
            { 2 , SKINSTATE.NOT_ACQUIRED }, 
            { 3 , SKINSTATE.ACQUIRED },
            { 4 , SKINSTATE.NOT_ACQUIRED },
            { 5 , SKINSTATE.ACQUIRED },
            { 6 , SKINSTATE.NOT_ACQUIRED },
        };
        _volume = 0.8f;
    }
}
