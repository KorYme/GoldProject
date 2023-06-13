using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayNextLevelButton : MonoBehaviour
{
    [SerializeField] Image _buttonImage;
    [SerializeField] GameObject _lockImage;

    public void NextLevelButton()
    {
        if (DataManager.Instance.CanPlayThisLevel(SceneManager.GetActiveScene().buildIndex))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnEnable()
    {
        if (!DataManager.Instance.CanPlayThisLevel(SceneManager.GetActiveScene().buildIndex))
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
