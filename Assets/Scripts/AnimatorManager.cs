using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ANIMATION_STATES
{
    IDLE,
    SLIDE_FACE,
    SLIDE_BACK,
    SLIDE_SIDE,
    WALLHIT_FACE,
    WALLHIT_BACK,
    WALLHIT_SIDE,
    REFLECTION,
    VICTORY,
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
        _currentState = ANIMATION_STATES.IDLE;
        ChangeAnimation(_currentState);
    }

    public void ChangeAnimation(ANIMATION_STATES state)
    {
        if (_currentState == state) return;
        _currentState = state;
        switch (state)
        {
            case ANIMATION_STATES.IDLE:
                _animator.Play($"Idle_{_playerName}");
                break;
            case ANIMATION_STATES.SLIDE_FACE:
                _animator.Play($"Slide_face_{_playerName}");
                break;
            case ANIMATION_STATES.SLIDE_BACK:
                _animator.Play($"Slide_dos_{_playerName}");
                break;
            case ANIMATION_STATES.SLIDE_SIDE:
                _animator.Play($"Slide_profil_{_playerName}");
                break;
            case ANIMATION_STATES.WALLHIT_FACE:
                break;
            case ANIMATION_STATES.WALLHIT_BACK:
                break;
            case ANIMATION_STATES.WALLHIT_SIDE:
                break;
            case ANIMATION_STATES.REFLECTION:
                break;
            case ANIMATION_STATES.VICTORY:
                break;
            default:
                break;
        }
    }
}
