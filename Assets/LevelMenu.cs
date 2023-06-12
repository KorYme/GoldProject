using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _skinMenu;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _skinButton;

    public void ReturnMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SettingsMenu()
    {
        _settingsButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SettingsButtonTween());
    }

    TweenCallback SettingsButtonTween()
    {
        _settingsMenu.SetActive(true);
        return null;
    }

    public void SkinMenu()
    {
        _skinButton.transform.DOScale(1.2f, 0.25f).SetLoops(2, LoopType.Yoyo).OnComplete(() => SkinMenuButtonTween());
    }
    
    TweenCallback SkinMenuButtonTween()
    {
        _skinButton.transform.localScale = new Vector3(1f, 1f, 1f);
        _skinMenu.SetActive(true);
        return null;
    }

    public void CloseSettingsMenu()
    {
        _settingsMenu.SetActive(false);
    }

    public void CloseCreditsMenu()
    {
        _settingsMenu.SetActive(true);
        _creditsMenu.SetActive(false);
    }

    public void CloseSkinMenu()
    {
        _skinMenu.SetActive(false);
    }

    public void CreditsMenu()
    {
        _settingsMenu.SetActive(false);
        _creditsMenu.SetActive(true);
    }
}
