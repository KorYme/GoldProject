using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [Header("TEST")]
    [SerializeField] Utilities.GAMECOLORS _color1;
    [SerializeField] Utilities.GAMECOLORS _color2;
    [SerializeField] LayerMask _testLayer;


    [Button]
    private void TestColorMix()
    {
        Debug.Log(Utilities.GetMixedColor(_color1, _color2).ToString());
    }

    [Button]
    private void TestColorSubstract()
    {
        Debug.Log(Utilities.GetSubtractedColor(_color1, _color2).ToString());
    }

    [Button]
    private void LayerTest()
    {
        _testLayer = LayerMask.GetMask("BasicWall", "Player");
    }

    [Button]
    private void Victory()
    {
        FindObjectsOfType<AnimatorManager>().ToList().ForEach(animator => animator.ChangeAnimation(ANIMATION_STATES.Victory));
    }
}
