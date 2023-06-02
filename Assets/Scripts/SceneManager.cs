using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneManager : MonoBehaviour
{
    public Action OnSceneUpdate
    {
        get; set;
    }

    protected virtual void Awake()
    {
        InputManager.Instance.SetUpNewLevel(this);
    }
}
