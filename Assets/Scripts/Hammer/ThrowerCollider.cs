using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            Destroy(collision.gameObject);
        }
    }
}
