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

    [Header("Tweening")]
    [SerializeField] private CanvasGroup _settingsCanvasGroup;
    [SerializeField] private CanvasGroup _skinCanvasGroup;
    [SerializeField] private CanvasGroup _leaderboardCanvasGroup;
    [SerializeField] private CanvasGroup _trophyCanvasGroup;

    [Header("Tweening Button")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _skinButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _trophyButton;



    bool _skinLevel = false;
    bool _settingsLevel = false;

    void Start()
    {
        DOTween.Init();
        _mainMenu.SetActive(true);
        _levelMenu.SetActive(false);
    }

    public void StartLevel()
    {
        _playButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => StarButtonTween());
    }

    TweenCallback StarButtonTween()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level-1");
        return null;
    }

    public void LevelMenu()
    {
        _levelButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => LevelButtonTween());
    }

    TweenCallback LevelButtonTween()
    {
        _mainMenu.SetActive(false);
        _levelMenu.SetActive(true);
        return null;
    }

    public void ReturnToMainMenu()
    {
        _mainMenu.SetActive(true);
        _levelMenu.SetActive(false);    
    }

    public void SettingsMenu()
    {
        _settingsButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SettingsButtonTween());
    }

    TweenCallback SettingsButtonTween()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
        _settingsCanvasGroup.DOFade(1, 1f);
        return null;
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
        _skinButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SkinMenuButtonTween());
    }
    
    TweenCallback SkinMenuButtonTween()
    {
        _mainMenu.SetActive(false);
        _skinMenu.SetActive(true);
        return null;
    }

    public void SettingsMenuFromLevel()
    {
        _settingsButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SettingsMenuFromLevelButtonTween());
    }

    TweenCallback SettingsMenuFromLevelButtonTween()
    {
        _settingsLevel = true;
        _settingsMenu.SetActive(true);
        return null;
    }

    public void SkinMenuFromLevel()
    {
        _settingsButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SkinMenuFromLevelButtonTween());
        _skinLevel = true;
        _skinMenu.SetActive(true);
    }

    TweenCallback SkinMenuFromLevelButtonTween()
    {
        _skinLevel = true;
        _skinMenu.SetActive(true);
        return null;
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
        _leaderboardButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => LeaderboardMenuButtonTween());
    }

    TweenCallback LeaderboardMenuButtonTween()
    {
        _mainMenu.SetActive(false);
        _leaderboardMenu.SetActive(true);
        return null;
    }

    public void ReturnToMainMenuFromLeaderboard()
    {
        _mainMenu.SetActive(true);
        _leaderboardMenu.SetActive(false);
    }

    public void TrophyMenu()
    {
        _trophyButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => TrophyMenuButtonTween());
    }

    TweenCallback TrophyMenuButtonTween()
    {
        _mainMenu.SetActive(false);
        _trophyMenu.SetActive(true);
        return null;
    }

    public void ReturnToMainMenuFromTrophy()
    {
        _mainMenu.SetActive(true);
        _trophyMenu.SetActive(false);
    }
}
