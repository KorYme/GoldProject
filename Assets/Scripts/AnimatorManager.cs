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

    public ANIMATION_STATES CurrentState
    {
        get; private set;
    }

    private void Start()
    {
        CurrentState = ANIMATION_STATES.Idle;
        ChangeAnimation(CurrentState);
    }

    public float ChangeAnimation(ANIMATION_STATES state, bool isForced = false)
    {
        if (!isForced && (CurrentState == ANIMATION_STATES.Victory || CurrentState == state)) return 0f;
        if (state == ANIMATION_STATES.Victory)
        {
            _animator.SetTrigger("Victory");
            return 2f;
        }
        CurrentState = state;
        _animator.Play($"{state}_{_playerName}");
        //Debug.Log($"{state} : {_animator.runtimeAnimatorController.animationClips.First(x => x.name == $"{state}_{_playerName}").length}");
        return _animator.runtimeAnimatorController.animationClips.First(x => x.name == $"{state}_{_playerName}").length;
    }
}