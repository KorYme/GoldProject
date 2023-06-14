using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackButton : MonoBehaviour
{
    [SerializeField] UnityEvent _onBackButtonUsed;
    
    private void Update()
    {
        if (_onBackButtonUsed != null)
        {
            _onBackButtonUsed?.Invoke();
        }
    }
}
