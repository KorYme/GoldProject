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
    [SerializeField] private GameObject _gameMenu;

    [SerializeField] private Image _elfImage;
    [SerializeField] private Image _nainImage;
    [SerializeField] private Image _ondineImage;

    [SerializeField] private GameObject _startMirror;
    [SerializeField] private GameObject _levelMirror;

    [SerializeField] private GameObject[] _laser;

    void Start()
    {
        DOTween.Init();
        _levelMenu.SetActive(false);
        _startMirror.SetActive(false);
        _levelMirror.SetActive(false);
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
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void ReturnToGameFromSettings()
    {
        _gameMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }
}
