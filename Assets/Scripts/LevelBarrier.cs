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
            if(DataManager.Instance.TotalStarNumber >= (15 * (i + 1)) - 7)
            {
                _levelBarrier[i].SetActive(false);
            }
        }    
    }

    private void Start()
    {
        for(int i = 0; i < _levelText.Length; i++)
        {
            _levelText[i].text = DataManager.Instance.TotalStarNumber + "/" + ((15 * (i + 1)) - 7).ToString();
        }
    }
}
