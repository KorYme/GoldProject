using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinMenuManager : MonoBehaviour
{
    [Header("Win Menu")]
    [SerializeField] LevelManager _levelManager;
    [SerializeField] private Image _starOne;
    [SerializeField] private TextMeshProUGUI _starOneText;
    [SerializeField] private Image _starTwo;
    [SerializeField] private TextMeshProUGUI _starTwoText;
    [SerializeField] private Image _starThree;
    [SerializeField] private TextMeshProUGUI _starThreeText;

    [SerializeField] private TextMeshProUGUI _moveText;

    [Header("Menu")]
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameMenu;

    [Header("Star Image")]
    [SerializeField] private Sprite _starGray;
    [SerializeField] private Sprite _starYellow;
    [SerializeField] private Sprite _starCyan;

    public void Win(int totalMove)
    {
        _winMenu.SetActive(true);
        _gameMenu.SetActive(false);
        UpdateWinMenu(_levelManager._LevelNumber , _levelManager._LevelPerfectScore, _levelManager._LevelThreeStarScore, _levelManager._LevelTwoStarScore, totalMove.ToString(), totalMove);
    }

    void UpdateWinMenu(int _levelNumber, int _levelPerfectScore, int _levelThreeStarScore, int _levelTwoStarScore,  string TextMove, int TotalMove)
    {
        _starThreeText.text = "Less than " + _levelThreeStarScore + " moves";

        _starTwoText.text = "Less than " + _levelTwoStarScore + " moves";

        _starOneText.text = "Finish the level"; 

        _moveText.text = "Level " + _levelNumber + " - " + TextMove + " moves";
        if(TotalMove <= _levelPerfectScore)
        {
            DataManager.Instance.CompleteALevel(_levelNumber, 4);
            _starOne.sprite = _starCyan;
            _starTwo.sprite = _starCyan;
            _starThree.sprite = _starCyan;
        }
        else if(TotalMove <= _levelThreeStarScore)
        {
            DataManager.Instance.CompleteALevel(_levelNumber, 3);
            _starOne.sprite = _starYellow;
            _starTwo.sprite = _starYellow;
            _starThree.sprite = _starYellow;
        }
        else if(TotalMove <= _levelTwoStarScore)
        {
            DataManager.Instance.CompleteALevel(_levelNumber, 2);
            _starOne.sprite = _starYellow;
            _starTwo.sprite = _starYellow;
            _starThree.sprite = _starGray;
        }
        else
        {
            DataManager.Instance.CompleteALevel(_levelNumber, 1);
            _starOne.sprite = _starYellow;
            _starTwo.sprite = _starGray;
            _starThree.sprite = _starGray;
        }
    }

    public void NextLevelButton()
    {
        NextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void NextLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
