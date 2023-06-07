using KorYmeLibrary.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IDataSaveable<GameData>
{
    [Header("Parameters")]
    [SerializeField] private int _levelPerStage;
    [SerializeField] private int _starSkippable;


    SerializableDictionnary<int, SKINSTATE> _skinDictionnary;
    SerializableDictionnary<int, int> _levelDictionnary;

    public SerializableDictionnary<int, int> LevelDictionnary
    {
        get => _levelDictionnary;
    }

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
    float _volume;

    public static DataManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There is already one DataManager in the scene");
            transform.parent = null;
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (LevelDictionnary == null)
        {
            _levelDictionnary = new();
        }
    }

    public void CompleteALevel(int levelID, int starsNumber)
    {
        if (starsNumber <= 0) return;
        _levelDictionnary[levelID] = Mathf.Clamp(starsNumber, _levelDictionnary.ContainsKey(levelID) ? _levelDictionnary[levelID] : 0, 4);
        TotalStarNumber += Mathf.Clamp(Mathf.Clamp(starsNumber, 0, 3) - Mathf.Clamp(_levelDictionnary.ContainsKey(levelID) ? _levelDictionnary[levelID] : 0, 0, 3), 0, 3);
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

    public bool CanPlayThisLevel(int level)
    {
        if (level < 0)
        {
            int currentStage = (level + 1) / -(_levelPerStage * 2);
            for (int i = 1; i <= _levelPerStage * 2; i++)
            {
                if (!LevelDictionnary.ContainsKey((currentStage * _levelPerStage * 2) + i) || LevelDictionnary[(currentStage * _levelPerStage * 2) + i] < 3) return false;
            }
            return true;
        }
        else
        {
            int value = 0;
            int levelStage = (level - 1) / _levelPerStage;
            for (int i = 1; i <= levelStage * _levelPerStage; i++)
            {
                if (!LevelDictionnary.ContainsKey(i)) continue;
                value += LevelDictionnary[i];
            }
            return value >= (levelStage * _levelPerStage * 3) - _starSkippable;
        }
    }
}
