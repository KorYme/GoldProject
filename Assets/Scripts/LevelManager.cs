using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Score")]
    [SerializeField] int _levelPerfectScore;
    public int LevelPerfectScore
    {
        get => _levelPerfectScore;
    }

    [SerializeField] int _levelThreeStarScore;
    public int LevelThreeStarScore
    {
        get => _levelThreeStarScore;
    }

    [SerializeField] int _levelTwoStarScore;
    public int LevelTwoStarScore
    {
        get => _levelTwoStarScore;
    }

    public int LevelNumber
    {
        get
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName.Contains("Level"))
            {
                return int.Parse(sceneName.Replace("Level-", ""));
            }
            else if (sceneName.Contains("Bonus"))
            {
                return int.Parse(sceneName.Replace("Bonus-", "")) * -1;
            }
            return 0;
        }
    }

    [Button]
    private void Display()
    {
        Debug.Log(LevelNumber);
    }
}
