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
    [SerializeField] Transform _starOne;
    [SerializeField] TextMeshProUGUI _starOneText;
    [SerializeField] Image _starOneImage;
    [SerializeField] Transform _starTwo;
    [SerializeField] TextMeshProUGUI _starTwoText;
    [SerializeField] Image _starTwoImage;
    [SerializeField] Transform _starThree;
    [SerializeField] TextMeshProUGUI _starThreeText;
    [SerializeField] Image _starThreeImage;

    [SerializeField] TextMeshProUGUI _perfectText;

    [SerializeField] TextMeshProUGUI _moveText;
    [SerializeField] TextMeshProUGUI _levelText;

    [Header("Menu")]
    [SerializeField] GameObject _winMenu;
    [SerializeField] GameObject _gameMenu;

    [Header("Star Image")]
    [SerializeField] Sprite _starGray;
    [SerializeField] Sprite _starYellow;
    [SerializeField] Sprite _starCyan;

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
        UpdateWinMenu(_levelManager.LevelNumber , _levelManager._LevelPerfectScore, _levelManager._LevelThreeStarScore, _levelManager._LevelTwoStarScore, totalMove.ToString(), totalMove);
    }

    void UpdateWinMenu(int _levelNumber, int _levelPerfectScore, int _levelThreeStarScore, int _levelTwoStarScore,  string TextMove, int TotalMove)
    {
        _starThreeText.text = "Less than " + _levelThreeStarScore + " moves";

        _starTwoText.text = "Less than " + _levelTwoStarScore + " moves";

        _starOneText.text = "Finish the level"; 

        _levelText.text = "Level " + _levelNumber;
        _moveText.text = TextMove + " moves";


        Vector3 starOnePosition = _starOne.transform.position;
        Vector3 starTwoPosition = _starTwo.transform.position;
        Vector3 starThreePosition = _starThree.transform.position;

        _starOneImage.sprite = _starYellow;
        _starTwoImage.sprite = _starYellow;
        _starThreeImage.sprite = _starYellow;

        _starOne.transform.position = new Vector3(-500, -500, 0);
        _starTwo.transform.position = new Vector3(-500, -500, 0);
        _starThree.transform.position = new Vector3(-500, -500, 0);

        if (TotalMove <= _levelPerfectScore)
        {
            _perfectText.text = "";
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, 4));
            DataManager.Instance.CompleteALevel(_levelNumber, 4);
        }
        else if (TotalMove <= _levelThreeStarScore)
        {
            _perfectText.text = "Need " + _levelPerfectScore + " moves for perfect";
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, 3));
            DataManager.Instance.CompleteALevel(_levelNumber, 3);
        }
        else if (TotalMove <= _levelTwoStarScore)
        {
            _starThreeImage.sprite = _starGray;
            _perfectText.text = "";
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, 2));
            DataManager.Instance.CompleteALevel(_levelNumber, 2);
        }
        else
        {
            _starTwoImage.sprite = _starGray;
            _starThreeImage.sprite = _starGray;
            _perfectText.text = "";
            StartCoroutine(UpdateStarSound(starOnePosition, starTwoPosition, starThreePosition, 1));
            DataManager.Instance.CompleteALevel(_levelNumber, 1);
        }
    }

    IEnumerator UpdateStarSound(Vector3 starOnePosition, Vector3 starTwoPosition, Vector3 starThreePosition, int numberOfStar)
    {
        if(numberOfStar >= 3)
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
            if (numberOfStar == 4)
            {
                AudioManager.Instance.PlaySound("StarPlat");
                yield return new WaitForSeconds(0.1f);
                _starOne.transform.DOPunchScale(new Vector3(2, 2, 2), 0.25f, 2, 0).OnComplete(() => _starOneImage.sprite = _starCyan);
                yield return new WaitForSeconds(0.25f);
                _starTwo.transform.DOPunchScale(new Vector3(2, 2, 2), 0.25f, 2, 0).OnComplete(() => _starTwoImage.sprite = _starCyan);
                yield return new WaitForSeconds(0.25f);
                _starThree.transform.DOPunchScale(new Vector3(2, 2, 2), 0.25f, 2, 0).OnComplete(() => _starThreeImage.sprite = _starCyan);
            }
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
        SceneManager.LoadScene(1);
        AudioManager.Instance.NbOfPlayersReflecting = 0;
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.NbOfPlayersReflecting = 0;
    }
}
