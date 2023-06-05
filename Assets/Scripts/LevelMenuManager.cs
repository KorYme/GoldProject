using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelMenuManager : MonoBehaviour
{
    public void LoadSpecificLevel(int level)
    {   
        LevelUIManager levelUIManager = GameObject.Find($"Level-{level}").GetComponent<LevelUIManager>();
        if(levelUIManager._canPlay)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene($"Level-{level}");
        }
    }


}
