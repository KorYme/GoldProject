using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManagerUI : MonoBehaviour
{
    [SerializeField] bool _isUnlocked = false;
    [SerializeField] private Image _skinImage;

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
