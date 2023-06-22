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
        if(_levelText == null)return;
        if(SceneManager.GetActiveScene().buildIndex - 2 > 50)
        {
            _levelText.text = "Bonus " + (SceneManager.GetActiveScene().buildIndex - 52);
            return;
        }
        _levelText.text = "Level " + (SceneManager.GetActiveScene().buildIndex - 2);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnButton();
        }
    }

    private void ReturnButton()
    {
        if (_pauseMenu != null && _pauseMenu.activeSelf)
        {
            ResumeButton();
        }
        else if (_creditMenu != null && _creditMenu.activeSelf)
        {
            BackButton();
        }
    }

    public void ResumeButton()
    {
        if (_gameMenu == null || _pauseMenu == null) return;
        _pauseMenu.SetActive(false);
        _gameMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CreditsMenuButton()
    {
        if (_creditMenu == null || _pauseMenu == null) return;
        _pauseMenu.SetActive(false);
        _creditMenu.SetActive(true);
    }

    public void BackButton()
    {
        if (_creditMenu == null || _pauseMenu == null) return;
        _pauseMenu.SetActive(true);
        _creditMenu.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenAchivement()
    {
#if UNITY_ANDROID
        AchievementManager.Instance.DisplayAchievementUI();
#endif
    }
}
