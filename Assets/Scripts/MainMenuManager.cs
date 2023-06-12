using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using KorYmeLibrary.SaveSystem;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levelMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _skinMenu;
    [SerializeField] private GameObject _trophyMenu;

    [Header("Tweening")]
    [SerializeField] private CanvasGroup _settingsCanvasGroup;
    [SerializeField] private CanvasGroup _skinCanvasGroup;
    [SerializeField] private CanvasGroup _trophyCanvasGroup;

    [Header("Tweening Button")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _skinButton;
    [SerializeField] private Button _trophyButton;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI _totalStarText;

    public void UpdateTotalStarText(int totalStarNumber)
    {
        _totalStarText.text = totalStarNumber.ToString() + "/192";
    }

    bool _skinLevel = false;
    bool _settingsLevel = false;

    void Start()
    {
        DOTween.Init();
        _mainMenu.SetActive(true);
        _levelMenu.SetActive(false);
        UpdateTotalStarText(DataManager.Instance.TotalStarNumber);
        DataManager.Instance.OnTotalStarChange += UpdateTotalStarText;
    }

    public void StartLevel()
    {
        _playButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => StarButtonTween());
    }

    TweenCallback StarButtonTween()
    {
        SceneManager.LoadScene($"Level-{GetContinueLevel()}");
        return null;
    }

    int GetContinueLevel()
    {
        // A DEBUG
        KeyValuePair<int, int> pair = new(0, 0);
        foreach (KeyValuePair<int, int> item in DataManager.Instance.LevelDictionnary)
        {
            if (item.Key <= 0 || item.Key < pair.Key || item.Value == 0) continue;
            pair = item;
        }
        if (DataManager.Instance.CanPlayThisLevel(pair.Key + 1)) return pair.Key + 1;
        foreach (KeyValuePair<int, int> item in DataManager.Instance.LevelDictionnary)
        {
            if (item.Key <= 0 || item.Key > pair.Key || item.Value >= 3) continue;
            pair = item;
        }
        return DataManager.Instance.CanPlayThisLevel(pair.Key) ? pair.Key : 1;
    }

    public void LevelMenu()
    {
        _levelButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => LevelButtonTween());
    }

    TweenCallback LevelButtonTween()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        return null;
    }

    public void SettingsMenu()
    {
        _settingsButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SettingsButtonTween());
    }

    TweenCallback SettingsButtonTween()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
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
        _skinButton.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);;
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
        _settingsButton.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);;
        _settingsLevel = true;
        _settingsMenu.SetActive(true);
        return null;
    }

    public void SkinMenuFromLevel()
    {
        _skinButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SkinMenuFromLevelButtonTween());
        _skinLevel = true;
        _skinMenu.SetActive(true);
    }

    TweenCallback SkinMenuFromLevelButtonTween()
    {
        _skinButton.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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

    public void TrophyMenu()
    {
        _trophyButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => TrophyMenuButtonTween());
    }

    TweenCallback TrophyMenuButtonTween()
    {
        _trophyButton.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);;
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
