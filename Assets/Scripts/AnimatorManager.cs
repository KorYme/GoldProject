using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ANIMATION_STATES
{
    Idle,
    Slide_face,
    Slide_dos,
    Slide_profil,
    Hit_face,
    Hit_dos,
    Hit_profil,
    Frein_face,
    Frein_dos,
    Frein_profil,
    Refuse,
    Reflection,
    Victory,
}

public class AnimatorManager : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] Animator _animator;

    [Header("Parameters")]
    [SerializeField] string _playerName;

    ANIMATION_STATES _currentState;

    private void Awake()
    {
        _currentState = ANIMATION_STATES.Idle;
        ChangeAnimation(_currentState);
    }

    public float ChangeAnimation(ANIMATION_STATES state)
    {
        if (_currentState == state) return 0f;
        _currentState = state;
        _animator.Play($"{state}_{_playerName}");
        return _animator.runtimeAnimatorController.animationClips.First(x => x.name == $"{state}_{_playerName}").length;
    }
}