using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LineRenderer _lineRenderer;

    public LineRenderer LineRenderer
    {
        get { return _lineRenderer; }
    }
}
