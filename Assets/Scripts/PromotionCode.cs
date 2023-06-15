using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KorYmeLibrary.SaveSystem;
using UnityEngine.SceneManagement;
using System;

public class PromotionCode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField inputField;

    string tmpValue = "";

    public void ValueChange(string str)
    {
        if ((str == "" || str ==null) && tmpValue.Length > 1)
        {
            str = tmpValue;
        }
        else
        {
            tmpValue = str;
        }
    }

    public void EndEdit(string str)
    {
        if (str == "" && tmpValue != "")
        {
            str = tmpValue;
            inputField.text = tmpValue;
        }
        PromotionCodeCheck(str);
    }

    public void PromotionCodeCheck(string str)
    {
        switch (str.ToUpper())
        {
            case "PULV":
                Debug.Log(inputField.text.ToUpper());
                DataManager.Instance.UnlockNewSkin(SKINPACK.PULV);
                break;
            case "ALLSTARS":
                Debug.Log(inputField.text.ToUpper());
                for (int i = 1; i < 51; i++)
                {
                    DataManager.Instance.CompleteALevel(i, 4);
                }
                for (int i = -1; i > -6; i--)
                {
                    DataManager.Instance.CompleteALevel(i, 4);
                }
                break;
            case "ALLSKINS":
                Debug.Log(inputField.text.ToUpper());
                for (int i = 0; i < 7; i++)
                {
                    DataManager.Instance.UnlockNewSkin((SKINPACK)i);
                }
                break;
            case "RESET":
                Debug.Log(inputField.text.ToUpper());
                DataManager.Instance.InitializeData();
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
