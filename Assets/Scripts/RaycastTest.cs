using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] GameObject _raycastTarget;
    LineRenderer _lineRenderer;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.startColor = Color.blue;
        _lineRenderer.endColor = Color.blue;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _raycastTarget.transform.position * 10000);

        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer);
            }
            else if (hit.collider.gameObject.CompareTag("Target"))
            {
               
                //Do whatever we want to do with the target of the laser
            }
        }
    }
}