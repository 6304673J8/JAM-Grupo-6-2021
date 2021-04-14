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
    private Animator animator;

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

    public Color fColor;

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
        animator = GetComponent<Animator>();
        switch (enemyType)
        {
            case Enemy.JUMPER:
                //fColor = new Color(214, 0, 0, 255);
                fColor = new Color(255, 255, 255, 255);
                sprRend.color = fColor;
                animator.SetInteger("EnemyType", 0);
                fColor = new Color(199, 164, 76, 255);
                break;
            case Enemy.WALKER:
                fColor = new Color(255, 255, 255, 255);
                //fColor = new Color(0, 152, 214, 255);
                sprRend.color = fColor;
                animator.SetInteger("EnemyType", 1);
                fColor = new Color(90, 92, 178, 255);
                break;
            case Enemy.DASHER:
                fColor = new Color(255, 255, 255, 255);
                //fColor = new Color(242, 229, 0, 255);
                sprRend.color = fColor;
                animator.SetInteger("EnemyType", 2);
                fColor = new Color(0, 165, 102, 255);
                break;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        if (playerDetected)
        {
            animator.SetBool("attack", true);
            if (enemyType == Enemy.JUMPER)
            {
                if (firstJ)
                {
                    if (totalTime >= 500)
                    {
                        totalTime = 0;
                        firstJ = false;
                        secondJ = true;
                        animator.SetBool("attack", false);
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
                        animator.SetBool("attack", false);
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
                        animator.SetBool("attack", false);
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
        if (direction == 1)
        {
            sprRend.flipX = false;
        }
        else
        {
            sprRend.flipX = true;
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
            switch (enemyType)
            {
                case Enemy.JUMPER:
                    fColor = new Color(209, 165, 0, 255);
                    break;
                case Enemy.WALKER:
                    fColor = new Color(0, 0, 244, 255);
                    break;
                case Enemy.DASHER:
                    fColor = new Color(0, 255, 0, 255);
                    break;
            }
            GameObject deathParticle = Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            deathParticle.gameObject.GetComponentInChildren<ParticleSystem>().startColor = fColor;
            Destroy(deathParticle, 3);
            Destroy(this.gameObject);
        }
    }
}
