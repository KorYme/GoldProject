using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using NaughtyAttributes;
using System.Linq;
using UnityEditor;

public class SkinManagerUI : MonoBehaviour
{
    [SerializeField] GameObject[] Skins;
    [SerializeField] GameObject[] boolSkins;

    void Start()
    {
        foreach (GameObject boolskins in boolSkins)
        {
            if(boolskins == true)
            {
                Debug.Log("true");
            }
        }
    }

    public void ChooseSkin(int SkinNumber)
    {

    }
}
