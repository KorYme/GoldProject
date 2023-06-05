using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinMenuManager : MonoBehaviour
{
    [Header("Win Menu")]
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

    public void Win(int TotalMove)
    {
        LevelManager levelManager = GetComponent<LevelManager>();
        string TextMove = TotalMove.ToString();
        _winMenu.SetActive(true);
        _gameMenu.SetActive(false);
        WinMenu(levelManager._levelNumber , levelManager._levelPerfectScore, levelManager._levelThreeStarScore, levelManager._levelTwoStarScore, levelManager._levelOneStarScore, TextMove, TotalMove);
    }

    void WinMenu(int _levelNumber, int _levelPerfectScore, int _levelThreeStarScore, int _levelTwoStarScore, int _levelOneStarScore, string TextMove, int TotalMove)
    {

        _starThreeText.text = "Less than " + _levelThreeStarScore + " moves";

        _starTwoText.text = "Less than " + _levelTwoStarScore + " moves";

        _starOneText.text = "Less than " + _levelOneStarScore + " moves";

        _moveText.text = "Level " + _levelNumber + " - " + TextMove + " moves";

        if(TotalMove <= _levelPerfectScore)
        {
            _starOne.sprite = _starCyan;
            _starTwo.sprite = _starCyan;
            _starThree.sprite = _starCyan;
        }
        else if(_levelPerfectScore > TotalMove || TotalMove <= _levelThreeStarScore)
        {
            _starOne.sprite = _starYellow;
            _starTwo.sprite = _starYellow;
            _starThree.sprite = _starYellow;
        }
        else if(_levelThreeStarScore > TotalMove || TotalMove <= _levelTwoStarScore)
        {
            _starOne.sprite = _starYellow;
            _starTwo.sprite = _starYellow;
            _starThree.sprite = _starGray;
        }
        else if(_levelOneStarScore <= TotalMove)
        {   
            _starOne.sprite = _starYellow;
            _starTwo.sprite = _starGray;
            _starThree.sprite = _starGray;
        }
    }

    public void NextLevelButton()
    {
        NextLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void NextLevel(int levelIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }

    public void RestartLevelButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
