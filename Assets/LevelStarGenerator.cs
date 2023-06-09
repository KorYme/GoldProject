using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KorYmeLibrary.SaveSystem;

public class LevelStarGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject _finishPoint;

    IEnumerator ShowStar()
    {
        if(DataManager.Instance.TotalStarNumber > 0)
        {
            yield return new WaitForSeconds(1f);
            foreach (GameObject star in _stars)
            {
                star.SetActive(true);
                star.transform.DOMove(_finishPoint.transform.position, 1f);
                star.transform.DOScale(0.75f, 1f);
            }
            ResetStar();
        }
    }   

    public void ShowStarCoroutine()
    {
        StartCoroutine(ShowStar());
    }

    public void ResetStar()
    {
        _stars[0].transform.position = new Vector3(-165, -1500, 0);
        _stars[1].transform.position = new Vector3(585, -1500, 0);
        _stars[2].transform.position = new Vector3(1335, -1500, 0);
    }
}
