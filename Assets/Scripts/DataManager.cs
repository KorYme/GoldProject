using DG.Tweening.Core;
using KorYmeLibrary.SaveSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour, IDataSaveable<GameData>
{
    [Header("Parameters")]
    [SerializeField] DSM_GameData dsm_GameData;
    [SerializeField] int _levelPerStage;
    [SerializeField] int _starSkippable;
    [SerializeField] int _lastLevelNumber;
    public int LastLevelNumber
    {
        get => _lastLevelNumber;
    }

    [Header("SaveParameters")]
    [SerializeField] bool _destroySaveOnNewVersion;

    public Action<int> OnTotalStarChange
    {
        get; set;
    }

    public Action<int> OnStarAdded
    {
        get; set;
    }

    public Action<int, int> OnLevelComplete
    {
        get; set;
    }

    // SKIN DICTIONNARIES
    List<SKINPACK> _skinAcquiredList;
    public List<SKINPACK> SkinAcquiredList
    {
        get => _skinAcquiredList;
        private set => _skinAcquiredList = value;
    }

    SerializableDictionnary<Utilities.GAMECOLORS, SKINPACK> _skinEquippedDictionnary;
    public SerializableDictionnary<Utilities.GAMECOLORS, SKINPACK> SkinEquippedDictionnary
    {
        get => _skinEquippedDictionnary;
        private set => _skinEquippedDictionnary = value;
    }


    // LEVEL DICTIONNARY
    SerializableDictionnary<int, int> _levelDictionnary;
    public SerializableDictionnary<int, int> LevelDictionnary
    {
        get => _levelDictionnary;
    }

    public int TotalStarNumber
    {
        get
        {
            if (LevelDictionnary == null) return 0;
            int value = 0;
            foreach (var item in LevelDictionnary)
            {
                if (item.Key > 0)
                {
                    value += Mathf.Clamp(item.Value, 0, 3);
                }
            }
            OnTotalStarChange?.Invoke(value);
            return value;
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
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    public void CompleteALevel(int levelID, int starsNumber)
    {
        if (starsNumber <= 0) return;
        if (!LevelDictionnary.ContainsKey(levelID))
        {
            LevelDictionnary[levelID] = 0;
        }
        OnLevelComplete?.Invoke(levelID, starsNumber);
        if (levelID > 0)
        {
            OnStarAdded?.Invoke(Mathf.Clamp(starsNumber - LevelDictionnary[levelID], 0, 4));
        }
        LevelDictionnary[levelID] += Mathf.Clamp(starsNumber - LevelDictionnary[levelID], 0, 4);
    }

    public void LoadData(GameData gameData)
    {
        if (_destroySaveOnNewVersion && gameData.Version != Application.version)
        {
            FileDataHandler<GameData>.DestroyOldData();
            dsm_GameData.NewGame();
            return;
        }
        _levelDictionnary = gameData.LevelDictionnary;
        _skinAcquiredList = gameData.SkinAcquiredDictionnary;
        _skinEquippedDictionnary = gameData.SkinEquippedDictionnary;
        _volume = gameData.Volume;
        CheckLevels();
    }


    public void SaveData(ref GameData gameData)
    {
        gameData.LevelDictionnary = _levelDictionnary;
        gameData.SkinAcquiredDictionnary = _skinAcquiredList;
        gameData.SkinEquippedDictionnary = _skinEquippedDictionnary;
        gameData.Volume = _volume;
        gameData.Version = Application.version;
    }

    public void InitializeData()
    {
        _levelDictionnary = new SerializableDictionnary<int, int>();
        _skinAcquiredList = new List<SKINPACK>()
        {
            SKINPACK.BASIC,
        };
        _skinEquippedDictionnary = new SerializableDictionnary<Utilities.GAMECOLORS, SKINPACK>
        {
            { Utilities.GAMECOLORS.Red, SKINPACK.BASIC },
            { Utilities.GAMECOLORS.Blue, SKINPACK.BASIC },
            { Utilities.GAMECOLORS.Yellow, SKINPACK.BASIC },
        };
        _volume = 0.8f;
    }

    public bool CanPlayThisLevel(int level)
    {
        if (level > _lastLevelNumber) return false;
        if (level < 0)
        {
            int currentStage = ((level + 1) * 10) / -(_levelPerStage * 2);
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

    private void CheckLevels()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (LevelDictionnary.ContainsKey(i) && !CanPlayThisLevel(i))
            {
                LevelDictionnary[i] = 0;
            }
        }
        for (int i = -1; i > -10; i--)
        {
            if (LevelDictionnary.ContainsKey(i) && !CanPlayThisLevel(i))
            {
                LevelDictionnary[i] = 0;
            }
        }
    }

    public void UnlockNewSkin(SKINPACK skin)
    {
        if (SkinAcquiredList.Contains(skin)) return;
        SkinAcquiredList.Add(skin);
    }
}
