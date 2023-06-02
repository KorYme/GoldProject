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
    [SerializeField] private int StarNumber;

    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameMenu;

    void WinMenu(string TextStar, string TextStarTwo, string TextStarThree, string TextMove, int StarNumber)
    {
        _winMenu.SetActive(true);
        _gameMenu.SetActive(false);
        _starOneText.text = TextStar;
        _starTwoText.text = TextStarTwo;
        _starThreeText.text = TextStarThree;
        _moveText.text = TextMove;


        switch(StarNumber)
            {
                case 1:
                    _starOne.color = Color.yellow;
                    break;
                case 2:
                    _starOne.color = Color.yellow;
                    _starTwo.color = Color.yellow;
                    break;
                case 3:
                    _starOne.color = Color.yellow;
                    _starTwo.color = Color.yellow;
                    _starThree.color = Color.yellow;
                    break;
                case 4:
                    _starOne.color = Color.cyan;
                    _starTwo.color = Color.cyan;
                    _starThree.color = Color.cyan;
                    break;
                default:
                    _starOne.color = Color.gray;
                    _starTwo.color = Color.gray;
                    _starThree.color = Color.gray;
                    break;
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
