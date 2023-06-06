using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Score")]
    public int _LevelPerfectScore;
    public int _LevelThreeStarScore;
    public int _LevelTwoStarScore;


    [Header("Level Number")]
    [Tooltip("Positive when normal level and negative for bonus")] public int _LevelNumber;

}
