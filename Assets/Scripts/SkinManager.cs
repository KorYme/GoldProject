using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using NaughtyAttributes;
using System.Linq;
using UnityEditor;

public class SkinManager : MonoBehaviour
{

    [SerializeField, Foldout("SkinOndine")] private GameObject _ondineSkin1;
    [SerializeField, Foldout("SkinOndine")] private GameObject _ondineSkin2;

    [SerializeField, Foldout("SkinNain")] private GameObject _nainSkin1;
    [SerializeField, Foldout("SkinNain")] private GameObject _nainSkin2;

    [SerializeField, Foldout("SkinElf")] private GameObject _elfSkin1;
    [SerializeField, Foldout("SkinElf")] private GameObject _elfSkin2;

    public void ChooseSkin(int SkinNumber)
    {
        switch (SkinNumber)
        {
            case 1:
                _ondineSkin1.SetActive(true);
                _ondineSkin2.SetActive(false);
                break;
            case 2:
                _ondineSkin1.SetActive(false);
                _ondineSkin2.SetActive(true);
                break;
            case 3:
                _nainSkin1.SetActive(true);
                _nainSkin2.SetActive(false);
                break;
            case 4:
                _nainSkin1.SetActive(false);
                _nainSkin2.SetActive(true);
                break;
            case 5:
                _elfSkin1.SetActive(true);
                _elfSkin2.SetActive(false);
                break;
            case 6:
                _elfSkin1.SetActive(false);
                _elfSkin2.SetActive(true);
                break;
        }
    }


}
