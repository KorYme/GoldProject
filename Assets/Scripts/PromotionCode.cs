using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KorYmeLibrary.SaveSystem;
using UnityEngine.SceneManagement;

public class PromotionCode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField inputField;

    public void PromotionCodeField()
    {
        switch (inputField.text.ToUpper())
        {
            case "PULV":
                DataManager.Instance.UnlockNewSkin(SKINPACK.PULV);
                break;
            case "ALLSTARS":
                for (int i = 1; i < 51; i++)
                {
                    DataManager.Instance.CompleteALevel(i, 4);
                }
                break;
            case "RESET":
                DataManager.Instance.InitializeData();
                Application.Quit();
                break;
            default:
                Debug.Log("No promotion code found");
                break;
        }
        inputField.text = string.Empty;
    }
}
