using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public enum Enemy { JUMPER, WALKER, DASHER }
    /*Creación variables*/
    // Variables de componentes
    private Rigidbody2D rb2d;
    private BoxCollider2D box2D;
    private SpriteRenderer sprRend;

    // Variables para modificar el personaje
    public float speed = 2;
    public int jumpForce = 3;
    public Enemy enemyType;

    public int direction;
    public bool playerDetected;
    private bool isJumping;
    private bool attackColdown;

    // Animation vars
    private bool firstJ;
    private bool secondJ;

    public GameObject deathParticles;


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
        sprRend = GetComponent<SpriteRenderer>();
        switch (enemyType)
        {
            case Enemy.JUMPER:
                sprRend.color = new Color(214, 0, 0, 255);
                break;
            case Enemy.WALKER:
                sprRend.color = new Color(0, 152, 214, 255);
                break;
            case Enemy.DASHER:
                sprRend.color = new Color(242, 229, 0, 255);
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        if (playerDetected)
        {
            if (enemyType == Enemy.JUMPER)
            {
                if (firstJ)
                {
                    if (totalTime >= 500)
                    {
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
            else if (enemyType == Enemy.DASHER)
            {
                if (firstJ)
                {
                    if (totalTime >= 500)
                    {
                        totalTime = 0;
                        firstJ = true;
                        playerDetected = false;
                        speed = 2;
                    }
                    speed = 3;
                    rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
                }
            }
            else
            {
                playerDetected = false;
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
            GameObject deathParticle = Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y), transform.rotation);
            Destroy(deathParticle, 3);
            Destroy(this.gameObject);
        }
    }
}
