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
    private void Victory()
    {
        FindObjectsOfType<AnimatorManager>().ToList().ForEach(animator => animator.ChangeAnimation(ANIMATION_STATES.Victory));
    }

    [Button]
    private void Get10FirstLevelsDone()
    {
        DataManager data = GetComponent<DataManager>();
        for (int i = 1; i < 11; i++)
        {
            data.CompleteALevel(i, 3);
        }
    }

    [Button]
    private void Get20OthersLevelsDone()
    {
        DataManager data = GetComponent<DataManager>();
        for (int i = 11; i < 21; i++)
        {
            data.CompleteALevel(i, 3);
        }
    }
}
