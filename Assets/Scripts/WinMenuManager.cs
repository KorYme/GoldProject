using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class WinMenuManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float _victoryScreenDelay;
    
    [Header("Win Menu")]
    [SerializeField] LevelManager _levelManager;
    [SerializeField] private GameObject _starOne;
    [SerializeField] private TextMeshProUGUI _starOneText;
    [SerializeField] private GameObject _starTwo;
    [SerializeField] private TextMeshProUGUI _starTwoText;
    [SerializeField] private GameObject _starThree;
    [SerializeField] private TextMeshProUGUI _starThreeText;

    [SerializeField] private TextMeshProUGUI _moveText;

    [Header("Menu")]
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameMenu;

    [Header("Star Image")]
    [SerializeField] private Sprite _starGray;
    [SerializeField] private Sprite _starYellow;
    [SerializeField] private Sprite _starCyan;

    bool _isLevelComplete;
    
    void Start()
    {
        DOTween.Init();
    }

    public void Win(int totalMove)
    {
        if (_isLevelComplete) return;
        AudioManager.Instance.NbOfPlayersReflecting = 0;
        _isLevelComplete = true;
        InputManager.Instance.DisableInputs();
        FindObjectsOfType<AnimatorManager>().ToList().ForEach(x => x.ChangeAnimation(ANIMATION_STATES.Victory));
        StartCoroutine(VictoryScreenAppearance(totalMove));
    }

    IEnumerator VictoryScreenAppearance(int totalMove)
    {
        yield return new WaitForSeconds(_victoryScreenDelay);
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

        Vector3 starOnePosition = _starOne.transform.position;
        Vector3 starTwoPosition = _starTwo.transform.position;
        Vector3 starThreePosition = _starThree.transform.position;

        _starOne.GetComponent<Image>().sprite = _starYellow;
        _starTwo.GetComponent<Image>().sprite = _starYellow;
        _starThree.GetComponent<Image>().sprite = _starYellow;

        _starOne.transform.position = new Vector3(0, 0, 0);
        _starTwo.transform.position = new Vector3(0, 0, 0);
        _starThree.transform.position = new Vector3(0, 0, 0);

        if(TotalMove <= _levelPerfectScore)
        {
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, true, 3));

            DataManager.Instance.CompleteALevel(_levelNumber, 4);
        }
        else if(TotalMove <= _levelThreeStarScore)
        {
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, false, 3));

            DataManager.Instance.CompleteALevel(_levelNumber, 3);
        }
        else if(TotalMove <= _levelTwoStarScore)
        {
            _starThree.GetComponent<Image>().sprite = _starGray;
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, false, 2));

            DataManager.Instance.CompleteALevel(_levelNumber, 2);
        }
        else
        {
            _starTwo.GetComponent<Image>().sprite = _starGray;
            _starThree.GetComponent<Image>().sprite = _starGray;
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, false, 1));

            DataManager.Instance.CompleteALevel(_levelNumber, 1);
        }
    }

    IEnumerator UpdateStarSound(Vector3 starOnePosition, Vector3 starTwoPosition, Vector3 starThreePosition, bool isPerfect, int numberOfStar)
    {
        if(numberOfStar == 3)
        {
            AudioManager.Instance.PlaySound("StarOne");
            _starOne.transform.DOMove(starOnePosition, 0.5f, false).OnComplete(() => 
            AudioManager.Instance.PlaySound("StarTwo"));
            yield return new WaitForSeconds(0.5f);
            _starTwo.transform.DOMove(starTwoPosition, 0.5f, false).OnComplete(() =>
            AudioManager.Instance.PlaySound("StarThree"));
            yield return new WaitForSeconds(0.5f);
            _starThree.transform.DOMove(starThreePosition, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
        else if(numberOfStar == 2)
        {
            AudioManager.Instance.PlaySound("StarOne");
            _starOne.transform.DOMove(starOnePosition, 0.5f, false).OnComplete(() => 
            AudioManager.Instance.PlaySound("StarTwo"));
            yield return new WaitForSeconds(0.5f);
            _starTwo.transform.DOMove(starTwoPosition, 0.5f, false).OnComplete(() =>
            AudioManager.Instance.PlaySound("StarFail"));
            yield return new WaitForSeconds(0.5f);
            _starThree.transform.DOMove(starThreePosition, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
        else if(numberOfStar == 1)
        {
            AudioManager.Instance.PlaySound("StarOne");
            _starOne.transform.DOMove(starOnePosition, 0.5f, false).OnComplete(() => 
            AudioManager.Instance.PlaySound("StarFail"));
            yield return new WaitForSeconds(0.5f);
            _starTwo.transform.DOMove(starTwoPosition, 0.5f, false).OnComplete(() =>
            AudioManager.Instance.PlaySound("StarFail"));
            yield return new WaitForSeconds(0.5f);
            _starThree.transform.DOMove(starThreePosition, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }

        if(isPerfect)
        {
            StartCoroutine(UpdateStarCyan());
        }
        
    }

    IEnumerator UpdateStarCyan()
    {
        AudioManager.Instance.PlaySound("StarPlat");
        yield return new WaitForSeconds(0.1f);
        _starOne.transform.DOPunchScale(new Vector3 (2, 2, 2), 0.25f, 2, 0).OnComplete(() => _starOne.GetComponent<Image>().sprite = _starCyan);
        yield return new WaitForSeconds(0.25f);
        _starTwo.transform.DOPunchScale(new Vector3 (2, 2, 2), 0.25f, 2, 0).OnComplete(() => _starTwo.GetComponent<Image>().sprite = _starCyan);
        yield return new WaitForSeconds(0.25f);
        _starThree.transform.DOPunchScale(new Vector3 (2, 2, 2), 0.25f, 2, 0).OnComplete(() => _starThree.GetComponent<Image>().sprite = _starCyan);
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
        AudioManager.Instance.NbOfPlayersReflecting = 0;
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.NbOfPlayersReflecting = 0;
    }

}
