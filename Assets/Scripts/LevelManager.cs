using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Level Score")]
    [SerializeField] public int _levelPerfectScore;
    [SerializeField] public int _levelThreeStarScore;
    [SerializeField] public int _levelTwoStarScore;
    [SerializeField] public int _levelOneStarScore;

    [Header("Level Number")]
    [SerializeField] public int _levelNumber;

}
