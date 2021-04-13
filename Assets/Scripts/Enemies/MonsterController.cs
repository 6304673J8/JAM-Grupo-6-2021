using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Animator animator;
    private int runParamID;
    private int attackParamID;
    private int deathParamID;

    public bool playerDetected;
    //movement
    public float speed = 2;
    public int direction;
    public float totalTime;

    private Rigidbody2D rb2d;
    private BoxCollider2D box2D;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        runParamID = Animator.StringToHash("isMoving");
        attackParamID = Animator.StringToHash("attacking");
        deathParamID = Animator.StringToHash("rip");

        playerDetected = false;
        direction = 1;
        totalTime = 0;

        rb2d = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = false;
        if (playerDetected)
        {
            animator.SetTrigger(attackParamID);
        }
        else
        {
            isRunning = true;
        }
        animator.SetBool(runParamID, isRunning);

    }
    /*private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
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
    public void StopMovement()
    {
        animator.SetBool(runParamID, );
    }*/
    private void changeDirection()
    {
        direction = direction * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hammer")
        {
            Destroy(this.gameObject, 3);
        }
    }
}
