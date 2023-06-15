using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, MinMaxSlider(0f, 15f)] Vector2 _animationTimeInBetween;

    [Header("References")]
    [SerializeField] int _animationsNumber;
    [SerializeField] Animator _animator;

    Coroutine _animationCoroutine;

    public void PlayAnimation()
    {
        if (_animationCoroutine != null) return;
        Debug.Log("HEY");
        _animationCoroutine = StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(_animationTimeInBetween.x, _animationTimeInBetween.y));
        _animator.SetTrigger("Trigger_" + Random.Range(0, _animationsNumber));
        _animationCoroutine = null;
    }
}
