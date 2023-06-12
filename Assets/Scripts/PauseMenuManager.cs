using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _soundSlider;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _creditMenu;

    [SerializeField] private TextMeshProUGUI _levelText;

    void Start()
    {
        _levelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ResumeButton()
    {
        _pauseMenu.SetActive(false);
        _gameMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void CreditsMenuButton()
    {
        _pauseMenu.SetActive(false);
        _creditMenu.SetActive(true);
    }

    public void BackButton()
    {
        _pauseMenu.SetActive(true);
        _creditMenu.SetActive(false);
    }

    public void RestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
