using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour
{
    public LayerMask groundLayers;
    private Rigidbody2D rb2d;
    private BoxCollider2D box2D;
    public Transform groundCheck;
    bool isFacingRight = true;

    // Variables para modificar el personaje
    public float speed = 2;
    public int jumpForce = 3;

    public int direction;
    public bool playerDetected;
    private bool attackColdown;

    // Animation vars
    private bool firstJ;
    private bool secondJ;
    public GameObject deathParticles;

    RaycastHit2D hit;
    public float totalTime;

    void Start()
    {
        direction = 1;
        totalTime = 0;
        playerDetected = false;
        firstJ = true;
        secondJ = false;
        rb2d = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        if (hit.collider != false)
        {
            if (playerDetected)
            {
                if (firstJ)
                {
                    if (totalTime >= 500)
                    {
                        totalTime = 0;
                        firstJ = true;
                        playerDetected = false;
                        speed = 4;
                    }
                    speed = 5;
                    rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
                }
                else
                {
                    playerDetected = false;
                }
            }
            else
            {
                if (totalTime >= 4000)
                {
                    totalTime = 0;
                    changeDirection();
                }
                rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
            }
            totalTime += delta;
        }
        else
        {
            Debug.Log("xd");
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        }
        totalTime += delta;
    }

    private void changeDirection()
    {
        direction = direction * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hammer")
        {
            GameObject deathParticle = Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Destroy(deathParticle, 3);
            Destroy(this.gameObject);
        }
    }
}
