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
    
    public void PauseMenu()
    {
        _gameMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToGameFromSettings()
    {
        _gameMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }
}
