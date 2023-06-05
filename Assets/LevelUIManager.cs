using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUIManager : MonoBehaviour
{
    [Header("Level")]
    public bool _canPlay = false;

    [Header("LevelText")]
    [SerializeField] private TextMeshProUGUI levelText;
    private int _starNumber;

    [Header("Stars")]
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    [Header("StarImage")]
    [SerializeField] private Sprite starImageGray;
    [SerializeField] private Sprite starImageYellow;
    [SerializeField] private Sprite starImageCyan;

    void Start()
    {
        string levelName = gameObject.name;
        levelName = levelName.Replace("Level-", "");
        levelName = levelName.Replace("Bonus-", "");
        levelText.text = levelName;
        UpdateStar(0);
    }

    public void UpdateStar(int _starNumber)
    {
        switch (_starNumber)
        {
            case 0:
                star1.sprite = starImageGray;
                star2.sprite = starImageGray;
                star3.sprite = starImageGray;
                break;
            case 1:
                star1.sprite = starImageGray;
                star2.sprite = starImageYellow;
                star3.sprite = starImageGray;
                break;
            case 2:
                star1.sprite = starImageYellow;
                star2.sprite = starImageGray;
                star3.sprite = starImageYellow;
                break;
            case 3:
                star1.sprite = starImageYellow;
                star2.sprite = starImageYellow;
                star3.sprite = starImageYellow;
                break;
            case 4:
                star1.sprite = starImageCyan;
                star2.sprite = starImageCyan;
                star3.sprite = starImageCyan;
                break;
        }
    }
}
