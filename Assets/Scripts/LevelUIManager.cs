using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelUIManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField, OnValueChanged(nameof(ChangeValues))] int _levelNumber;
    [SerializeField, OnValueChanged(nameof(ChangeValues))] bool _isABonusLevel;

    public int LevelNumber
    {
        get => _levelNumber * (_isABonusLevel ? -1 : 1);
    }

    public bool CanPlay
    {
        get => DataManager.Instance.CanPlayThisLevel(LevelNumber);
    }

    [SerializeField, Foldout("References")] private TextMeshProUGUI _levelText;
    [SerializeField, Foldout("References")] private Image _buttonImage;

    [SerializeField, Foldout("Image")] private Sprite _normalLevel;
    [SerializeField, Foldout("Image")] private Sprite _bonusLevel;

    [SerializeField, Foldout("StarImages")] private Image _star1;
    [SerializeField, Foldout("StarImages")] private Image _star2;
    [SerializeField, Foldout("StarImages")] private Image _star3;

    [SerializeField, Foldout("StarSprites")] private Sprite _starImageGray;
    [SerializeField, Foldout("StarSprites")] private Sprite _starImageYellow;
    [SerializeField, Foldout("StarSprites")] private Sprite _starImageCyan;

    void Start()
    {
        _levelText.text = _levelNumber.ToString();
        if (DataManager.Instance == null)
        {
            Debug.Log("Pas trouv� l'instance");
        }
        if (DataManager.Instance.LevelDictionnary == null)
        {
            Debug.Log("Pas trouv� le dictionnaire");
        }
        UpdateStar(DataManager.Instance.LevelDictionnary.ContainsKey(LevelNumber) ? DataManager.Instance.LevelDictionnary[LevelNumber] : 0);
    }

    public void UpdateStar(int starNumber)
    {
        switch (starNumber)
        {
            case 0:
                _star1.sprite = _starImageGray;
                _star2.sprite = _starImageGray;
                _star3.sprite = _starImageGray;
                break;
            case 1:
                _star1.sprite = _starImageGray;
                _star2.sprite = _starImageYellow;
                _star3.sprite = _starImageGray;
                break;
            case 2:
                _star1.sprite = _starImageYellow;
                _star2.sprite = _starImageGray;
                _star3.sprite = _starImageYellow;
                break;
            case 3:
                _star1.sprite = _starImageYellow;
                _star2.sprite = _starImageYellow;
                _star3.sprite = _starImageYellow;
                break;
            case 4:
                _star1.sprite = _starImageCyan;
                _star2.sprite = _starImageCyan;
                _star3.sprite = _starImageCyan;
                break;
        }
    }

    public void LoadLevel()
    {
        if (!CanPlay) return;
        if (SceneManager.GetSceneByName(_isABonusLevel ? "Bonus-" : "Level-" + _levelNumber.ToString()) == null)
        {
            Debug.Log("Sc�ne pas trouv�");
            return;
        }
        SceneManager.LoadScene(_isABonusLevel ? "Bonus-" : "Level-" + _levelNumber.ToString());
    }

    private void ChangeValues()
    {
        name = (_isABonusLevel ? "Bonus-" : "Level-") + _levelNumber.ToString();
        _levelText.text = _levelNumber.ToString();
        _buttonImage.sprite = _isABonusLevel ? _bonusLevel : _normalLevel;
    }

    [Button]
    private void IncrementLevelValue()
    {
        _levelNumber++;
        ChangeValues();
    }

    [Button]
    public void SetUpVariables()
    {
        if (_levelText == null)
        {
            _levelText = transform.GetComponentInChildren<TextMeshProUGUI>();
        }
        if (_buttonImage == null)
        {
            _buttonImage = GetComponent<Image>();
        }
        ChangeValues();
    }

    [Button]
    private void UpdateAll()
    {
        FindObjectsOfType<LevelUIManager>().ToList().ForEach(x => x.SetUpVariables());
    }
}