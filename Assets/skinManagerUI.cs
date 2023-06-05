using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skinManagerUI : MonoBehaviour
{
    public bool _isUnlocked = false;
    [SerializeField] private Image _skinImage;
    [SerializeField] private Image _skinSelection;

    void Start()
    {
        if (_isUnlocked)
        {
            _skinImage.color = Color.white;
        }
        else
        {
            _skinImage.color = Color.gray;
        }
    }
}
