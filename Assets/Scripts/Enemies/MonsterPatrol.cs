using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;
    public LayerMask groundLayers;

    public Transform groundCheck;
    bool isFacingRight = true;

    RaycastHit2D hit;
}
