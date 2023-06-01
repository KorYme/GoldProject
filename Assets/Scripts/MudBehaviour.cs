using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("OnlyPlayers");
        }
    }
}
