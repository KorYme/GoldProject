using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitByRay : MonoBehaviour
{
    Vector3 _reflectDir;
    LineRenderer _lineRenderer;
    RaycastHit2D hit;
    Color _orange = new Color(1.0f, 0.64f, 0.0f);

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PlayerColor _playerColor;
    [SerializeField] Transform _crystal;

    private void Start()
    {
        switch (_playerColor)
        {
            case PlayerColor.Blue:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case PlayerColor.Yellow:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case PlayerColor.Red:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.enabled = true;
    }

    public void HitByRay(LineRenderer _incomingRay)
    {
        //_lineRenderer.enabled = (Input.touchCount != 0);

        hit = Physics2D.Raycast(_crystal.position, _crystal.position - transform.position, Mathf.Infinity, _layerMask);
        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.CompareTag("Target"))
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        
        _lineRenderer.SetPosition(0, _crystal.position);
        _lineRenderer.SetPosition(1, hit.point);
        _lineRenderer.startColor = OutgoingRayColor(_incomingRay);
        _lineRenderer.endColor = OutgoingRayColor(_incomingRay);
    }
    
    private Color OutgoingRayColor(LineRenderer _incomingRay)
    {
        switch (_playerColor)
        {
            case PlayerColor.Blue:
                switch (_incomingRay.endColor)
                {
                    case Color color when color == Color.red:
                        return Color.magenta;
                    case Color color when color == Color.yellow:
                        return Color.green;
                    case Color color when color == Color.white:
                        return Color.blue;
                    case Color color when color == _orange:
                        return Color.white;
                    default:
                        return Color.white;
                }
            case PlayerColor.Yellow:
                switch (_incomingRay.endColor)
                {
                    case Color color when color == Color.red:
                        return _orange;
                    case Color color when color == Color.blue:
                        return Color.green;
                    case Color color when color == Color.white:
                        return Color.yellow;
                    case Color color when color == Color.magenta:
                        return Color.white;
                    default:
                        return Color.white;
                }
            case PlayerColor.Red:
                switch (_incomingRay.endColor)
                {
                    case Color color when color == Color.blue:
                        return Color.magenta;
                    case Color color when color == Color.yellow:
                        return _orange;
                    case Color color when color == Color.white:
                        return Color.red;
                    case Color color when color == Color.green:
                        return Color.white;
                    default:
                        return Color.white;
                }
            default:
                return Color.white;
        }
    }
    
    private enum PlayerColor
    {
        Blue,Yellow,Red
    }
}
