using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private TextMeshProUGUI _moveText;

    private void Start()
    {
        InputManager.Instance.SetUpNewLevel(this);
    }

    public void PauseMenu()
    {
        _gameMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void LevelMenuButton()
    {
        SceneManager.LoadScene("Level Menu");
    }

    public void ReturnToGameFromSettings()
    {
        _gameMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void UpdateMoveText(int moves)
    {
        string moveText = moves.ToString();
        if (moves == 1)
        {
            _moveText.text = moveText + " Move";
        }
        else
        {
            _moveText.text = moveText + " Moves";
        }

       if (moves == 999)
       {
           ReloadLevel();
       }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
