using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Score")]
    public int _LevelPerfectScore;
    public int _LevelThreeStarScore;
    public int _LevelTwoStarScore;

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
                return -int.Parse(sceneName.Replace("Bonus-", ""));
            }
            return 0;
        }
    }
}
