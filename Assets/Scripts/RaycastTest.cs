using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] GameObject _raycastTarget;
    float _playerHitOffset;


    private void Start()
    {
        
    }

    private void Update()
    {

        if (_raycastTarget.transform.position.x > transform.position.x)
            _playerHitOffset = -1;
        else
            _playerHitOffset = 1;


        Ray ray = new Ray(transform.position, _raycastTarget.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.white);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Ray ray2 = new Ray(new Vector2(hit.point.x - _playerHitOffset, hit.point.y), hit.collider.gameObject.GetComponent<Player>().GetReflectDir() - hit.point);
                if (Physics.Raycast(ray2, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        //Do whatever we want to do with the target of the laser
                    }
                    Debug.DrawLine(ray2.origin, hit.point, Color.red);
                }
                else
                {
                    Debug.DrawLine(ray2.origin, ray2.origin + ray2.direction * 1000, Color.green);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Target"))
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                //Do whatever we want to do with the target of the laser
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1000, Color.green);
        }
    }
}