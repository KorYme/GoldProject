using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] GameObject _raycastTarget;

    private void Update()
    {

        Ray ray = new Ray(transform.position, _raycastTarget.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.white);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Ray ray2 = new Ray(hit.point, hit.collider.gameObject.GetComponent<Player>().GetMousePos() - hit.point);
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
                //Do whatever we want to do with the target of the laser
            }

        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1000, Color.green);
        }
    }
}
