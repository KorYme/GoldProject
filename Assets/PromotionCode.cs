using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromotionCode : MonoBehaviour
{
    public TMP_InputField inputField;

    public void PromotionCodeField()
    {
        if (inputField.text == "IIM")
        {
            //DataManager.Instance.UnlockNewSkin();
        }
    }
}
