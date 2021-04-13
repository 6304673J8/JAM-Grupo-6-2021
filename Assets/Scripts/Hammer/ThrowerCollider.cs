using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerCollider : MonoBehaviour
{
    public GameObject throwerVisible;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            GetComponent<PlayerController>().hammer = true;
            throwerVisible.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}
