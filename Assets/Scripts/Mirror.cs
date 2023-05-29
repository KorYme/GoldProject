using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    LineRenderer _lineRenderer;
    GameObject _tempPlayer;
    GameObject _currentPlayer;
    Vector3 _reflectTarget;
    Vector3 _reflectStartPoint;
    bool _shouldReflectLaser = false;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.enabled = true;
    }

    public void HitByLaser(LineRenderer _hitLaser, Vector3 _hitPoint, bool _shouldShoot)
    {
        _lineRenderer.startColor = _hitLaser.startColor;
        _lineRenderer.endColor = _hitLaser.endColor;
        _reflectTarget = Vector3.Reflect(_hitLaser.GetPosition(1) - _hitLaser.GetPosition(0), transform.up);
        _reflectStartPoint = _hitPoint;
        _shouldReflectLaser = _shouldShoot;
    }

    private void Update()
    {
        if (_shouldReflectLaser)
        {
            RaycastHit2D hit = Physics2D.Raycast(_reflectStartPoint, _reflectTarget);
            
            if (hit.collider != null)
            {
                _lineRenderer.SetPosition(0, _reflectStartPoint);
                _lineRenderer.SetPosition(1, hit.point);

                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    _tempPlayer = hit.collider.gameObject;
                    if (_currentPlayer == null)
                    {
                        _currentPlayer = _tempPlayer;
                    }

                    if (_tempPlayer != _currentPlayer)
                    {
                        _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, false);
                        _currentPlayer = _tempPlayer;
                        _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, true);
                    }
                    else
                    {
                        _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, true);
                    }
                }
                else if (hit.collider.gameObject.CompareTag("Mirror"))
                {
                    _tempPlayer = hit.collider.gameObject;
                    if (_currentPlayer == null)
                    {
                        _currentPlayer = _tempPlayer;
                    }

                    if (_tempPlayer != _currentPlayer)
                    {
                        _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, false);
                        _currentPlayer = _tempPlayer;
                        _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, true);
                    }
                    else
                    {
                        _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, true);
                    }
                }
                else
                {
                    if (_currentPlayer != null)
                    {
                        if (_currentPlayer.GetComponent<Mirror>() != null)
                        {
                            _currentPlayer.GetComponent<Mirror>().HitByLaser(_lineRenderer, hit.point, false);
                            _currentPlayer = null;
                        }
                        else if (_currentPlayer.GetComponent<PlayerHitByRay>() != null)
                        {
                            _currentPlayer.GetComponent<PlayerHitByRay>().HitByRay(_lineRenderer, false);
                            _currentPlayer = null;
                        }

                    }
                }
            }
            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
