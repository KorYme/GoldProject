using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _skinMenu;
    [SerializeField] private GameObject _leaderboardMenu;
    [SerializeField] private GameObject _trophyMenu;
    
    

    [SerializeField] private Image _elfImage;
    [SerializeField] private Image _nainImage;
    [SerializeField] private Image _ondineImage;

    [SerializeField] private GameObject _startMirror;
    [SerializeField] private GameObject _levelMirror;

    [SerializeField] private GameObject[] _laser;

    bool _skinLevel = false;
    bool _settingsLevel = false;

    void Start()
    {
        DOTween.Init();
        _mainMenu.SetActive(true);
        _levelMenu.SetActive(false);
    }

    public void UpdateStartMirror()
    {
        //_startMirror.SetActive(true);
        //_startMirror.transform.DOMoveX(60, 1f);
        //_levelMirror.transform.DOMoveX(-250, 1f);
        //_levelMirror.SetActive(false);
        StartGame();
    }

    public void UpdateLevelMirror()
    {
        //_levelMirror.SetActive(true);
        //_levelMirror.transform.DOMoveX(60, 1f);
        //_startMirror.transform.DOMoveX(-250, 1f);
        //_startMirror.SetActive(false);

        _mainMenu.SetActive(false);
        _levelMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        _mainMenu.SetActive(true);
        _levelMenu.SetActive(false);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level-1");
    }

    public void SettingsMenu()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void ReturnToMainMenuFromSettings()
    {
        if(_settingsLevel)
        {
            _settingsLevel = false;
            _settingsMenu.SetActive(false);
        }
        else
        {
            _settingsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }

    public void SkinMenu()
    {
        _mainMenu.SetActive(false);
        _skinMenu.SetActive(true);
    }

    public void SettingsMenuFromLevel()
    {
        _settingsLevel = true;
        _settingsMenu.SetActive(true);
    }

    public void SkinMenuFromLevel()
    {
        _skinLevel = true;
        _skinMenu.SetActive(true);
    }

    public void ReturnToMainMenuFromSkin()
    {
        if (_skinLevel)
        {
            _skinLevel = false;
            _skinMenu.SetActive(false);
        }
        else
        {
            _skinMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }

    public void LeaderboardMenu()
    {
        _mainMenu.SetActive(false);
        _leaderboardMenu.SetActive(true);
    }

    public void ReturnToMainMenuFromLeaderboard()
    {
        _mainMenu.SetActive(true);
        _leaderboardMenu.SetActive(false);
    }

    public void TrophyMenu()
    {
        _mainMenu.SetActive(false);
        _trophyMenu.SetActive(true);
    }

    public void ReturnToMainMenuFromTrophy()
    {
        _mainMenu.SetActive(true);
        _trophyMenu.SetActive(false);
    }
}
