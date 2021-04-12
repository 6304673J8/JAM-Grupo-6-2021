using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    /*Creación variables*/
    // Variables de componentes
    private Rigidbody2D rb2d;
    private BoxCollider2D box2D;

    // Variables para modificar el personaje
    public float speed = 2;
    public int jumpForce = 3;

    public int direction;
    public bool playerDetected;
    private bool isJumping;
    private bool attackColdown;

    // Animation bools
    private bool firstJ;
    private bool secondJ;


    public float totalTime;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        if (playerDetected)
        {
            if (firstJ)
            {
                if (totalTime >= 500) {
                    totalTime = 0;
                    firstJ = false;
                    secondJ = true;
                }
                rb2d.velocity = new Vector2(direction * speed, jumpForce);
            }
            else if (secondJ)
            {
                if (totalTime >= 2000)
                {
                    totalTime = 0;
                    firstJ = true;
                    secondJ = false;
                    playerDetected = false;
                }
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            }
        }
        else
        {
            // Cambia Dirección
            
            if (totalTime >= 2000)
            {
                totalTime = 0;
                changeDirection();
            }
            
            rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
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

        }
    }
}
