using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _soundSlider;
    [SerializeField] private GameObject _vibrationToggle;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameMenu;

    public void ResumeButton()
    {
        _pauseMenu.SetActive(false);
        _gameMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreditsMenuButton()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
