using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelMenuManager : MonoBehaviour
{
    public void LoadSpecificLevel(int level)
    {
        transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => LoadSpecificLevelButtonTween(level));
    }

    TweenCallback LoadSpecificLevelButtonTween(int level)
    {
        LevelUIManager levelUIManager = GameObject.Find($"Level-{level}").GetComponent<LevelUIManager>();
        if(levelUIManager.CanPlay)
        {
            SceneManager.LoadScene($"Level-{level}");
        }
        return null;
    }

    public void LoadSpecificBonusLevel(int level)
    {   
        transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => LoadSpecificBonusLevelButtonTween(level));
    }

    TweenCallback LoadSpecificBonusLevelButtonTween(int level)
    {
        LevelUIManager levelUIManager = GameObject.Find($"Bonus-{level}").GetComponent<LevelUIManager>();
        if(levelUIManager.CanPlay)
        {
            SceneManager.LoadScene($"Bonus-{level}");
        }
        return null;
    }
}
