using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnAnimation : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SpawnAnimationCoroutine());
    }

    IEnumerator SpawnAnimationCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
