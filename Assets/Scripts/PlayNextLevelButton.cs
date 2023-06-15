using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayNextLevelButton : MonoBehaviour
{
    [SerializeField] Image _buttonImage;
    [SerializeField] GameObject _lockImage;
    [SerializeField] LevelManager _levelManager;

    public void NextLevelButton()
    {
        int currentLevel = _levelManager.LevelNumber;
        if (currentLevel == -5)
        {
            SceneManager.LoadScene("Level Menu");
        }
        else if (currentLevel == 50)
        {
            if (DataManager.Instance.CanPlayThisLevel(-1))
            {
                SceneManager.LoadScene("Bonus-1");
            }
        }
        else if (currentLevel > 0)
        {
            if (DataManager.Instance.CanPlayThisLevel(currentLevel + 1))
            {
                SceneManager.LoadScene($"Level-{currentLevel + 1}");
            }
        }
        else
        {
            if (DataManager.Instance.CanPlayThisLevel(-currentLevel -1))
            {
                SceneManager.LoadScene($"Bonus-{-currentLevel - 1}");
            }
        }
    }

    private void OnEnable()
    {
        int nextLevel = _levelManager.LevelNumber;
        if (nextLevel == 50)
        {
            nextLevel = -1;
        }
        else if (nextLevel > 0)
        {
            nextLevel++;
        }
        else
        {
            nextLevel--;
        }
        if (!DataManager.Instance.CanPlayThisLevel(nextLevel))
        {
            _buttonImage.color = Color.grey;
            _lockImage.SetActive(true);
        }
        else
        {
            _buttonImage.color = Color.white;
            _lockImage.SetActive(false);
        }
    }
}
