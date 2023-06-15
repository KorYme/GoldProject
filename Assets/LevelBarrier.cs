using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelBarrier : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelBarrier;
    [SerializeField] private TextMeshProUGUI[] _levelText;

    void Awake() 
    {
        for(int i = 0; i < _levelBarrier.Length; i++)
        {
            if(DataManager.Instance.TotalStarNumber >= 8 * (i + 1))
            {
                _levelBarrier[i].SetActive(false);
            }
        }    
    }

    private void Start()
    {
        int k = 5;
        int j = 0;
        for(int i = 0; i < _levelText.Length; i++)
        {
            j = (k * 3) - 7;
            _levelText[i].text = DataManager.Instance.TotalStarNumber + "/" + j;
            k = k + 5;
        }
    }
}
