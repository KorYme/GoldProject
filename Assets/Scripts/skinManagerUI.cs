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

    void Start()
    {
        for (int i = 3; i < Skins.Length; i++)
        {
            Skins[i].GetComponentInChildren<Image>().color = Color.gray;
        }
    }
    
    public void UnlockedSkin(bool Unlocked)
    {
        if(Unlocked)
        {
            GameObject SkinName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            for (int i = 0; i < Skins.Length; i++)
            {
                if (SkinName.name == Skins[i].name)
                {
                    Skins[i].GetComponentInChildren<Image>().color = Color.white;
                }
            }

        }
    }
}
