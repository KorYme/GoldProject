using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TMP_Text _text;

    private void Start()
    {
        _text.text = DataManager.Instance.TotalStarNumber.ToString();
    }
}
