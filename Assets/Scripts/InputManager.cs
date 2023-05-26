using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController _playerController;

    public bool CanGetNewInput
    {
        get => _playerController == null ? true : _playerController.ControllerCoroutine == null;
    }
}
