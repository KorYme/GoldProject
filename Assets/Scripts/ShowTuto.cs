using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTuto : MonoBehaviour
{
    [SerializeField] private GameObject[] _tutoPage;
    [SerializeField] private GameObject _tutoCanvas;
    [SerializeField] private bool _tutoPageOne;
    [SerializeField] private bool _tutoPageTwo;
    [SerializeField] private bool _tutoPageThree;
    [SerializeField] private bool _tutoPageFour;
    [SerializeField] private bool _tutoPageFive;
    [SerializeField] private bool _tutoPageSix;
    [SerializeField] private bool _tutoPageSeven;
    [SerializeField] private bool _tutoPageEight;
    [SerializeField] private bool _tutoPageNine;

    private void Start()
    {
        if(_tutoPageOne)
        {
            ShowTutoCanvas();
            _tutoPage[0].SetActive(true);
        }
        else if (_tutoPageTwo)
        {
            ShowTutoCanvas();
            _tutoPage[1].SetActive(true);
        }
        else if (_tutoPageThree)
        {
            ShowTutoCanvas();
            _tutoPage[2].SetActive(true);
        }
        else if (_tutoPageFour)
        {
            ShowTutoCanvas();
            _tutoPage[3].SetActive(true);
        }
        else if (_tutoPageFive)
        {
            ShowTutoCanvas();
            _tutoPage[4].SetActive(true);
        }
        else if (_tutoPageSix)
        {
            ShowTutoCanvas();
            _tutoPage[5].SetActive(true);
        }
        else if (_tutoPageSeven)
        {
            ShowTutoCanvas();
            _tutoPage[6].SetActive(true);
        }
        else if (_tutoPageEight)
        {
            ShowTutoCanvas();
            _tutoPage[7].SetActive(true);
        }
        else if (_tutoPageNine)
        {
            ShowTutoCanvas();
            _tutoPage[8].SetActive(true);
        }

    }

    void ShowTutoCanvas()
    {
        _tutoCanvas.SetActive(true);
    }

}
