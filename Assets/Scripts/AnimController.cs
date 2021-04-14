using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AnimController : MonoBehaviour
{
    /*Creación variables*/
    // Variables de componentes
    private Rigidbody2D rb2d;
    private BoxCollider2D box2D;
    private SpriteRenderer sprRend;
    private bool m_FacingRight = true;

    // Variables para modificar el personaje
    public float speed = 2;
    public int jumpForce = 3;

    public bool hammer;
    public int direction;
    public bool isJumping;
    private Vector3 startPosition;

    public GameObject deathbody;
    public GameObject levelLoader;
    private GameObject deathbodyToCrash;

    //ToCopy Animation
    private bool m_Grounded;            // Whether or not the player is grounded.

    public Animator animator;
    private int idleNoHammerID;
    private int runHammerID;
    private int runNoHammerID;
    private int jumpHammerID;
    private int jumpNoHammerID;
    bool hasJumped = false;
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnHammerEvent;
    private bool m_wasThrowing = false;

    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnHammerEvent == null)
            OnHammerEvent = new BoolEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        rb2d = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        sprRend = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        hammer = true;

        //ToCopy Animation
        animator = GetComponent<Animator>();
        /*idleNoHammerID = Animator.StringToHash("")    ;
        runNoHammerID  = Animator.StringToHash("")    ;
        jumpNoHammerID = Animator.StringToHash("")    ;
        attackHammerID = Animator.StringToHash("doHammer");
        */
        runHammerID = Animator.StringToHash("isHammerMoving");
        jumpHammerID   = Animator.StringToHash("isHammerJumping");
    }

    private void Update()
    {
        //bool hasJumped = false;
        if (Input.GetKey("space") && !isJumping)
        {
            Destroy(deathbodyToCrash);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            hasJumped = true;
            
            m_Grounded = false; 
            animator.SetBool(jumpHammerID, true);

        }

        if (Input.GetKeyDown("f") && hammer)
        {
            GameObject deathBody = Instantiate(deathbody, new Vector3(transform.position.x, transform.position.y, 1f), transform.rotation);
            transform.position = startPosition;
        }
        //animator.SetBool(jumpHammerID, hasJumped);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;

        bool isMoving = false;

        if (!wasGrounded)
            OnLandEvent.Invoke();
        if (Input.GetKey("d"))
        {
            isMoving = true;
            direction = 1;
            if (direction > 0 && !m_FacingRight)
                Flip();
        }
        else if (Input.GetKey("a"))
        {
            isMoving = true;

            direction = -1;
            if (direction < 0 && m_FacingRight)
                Flip();
        }
        else
        {
            direction = 0;
        }
        animator.SetBool(runHammerID, isMoving);

        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Scenario")
        {
            m_Grounded = true;

            OnLandEvent.Invoke();
            if (isJumping)
            {
                m_Grounded = false;

                bool col1 = false;
                bool col2 = false;
                bool col3 = false;

                float center_x = (box2D.bounds.min.x + box2D.bounds.max.x) / 2;
                Vector2 centerPosition = new Vector2(center_x, box2D.bounds.min.y);
                Vector2 leftPosition = new Vector2(box2D.bounds.min.x + 0.1f, box2D.bounds.min.y);
                Vector2 rightPosition = new Vector2(box2D.bounds.max.x - 0.1f, box2D.bounds.min.y);

                RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, Vector2.down, 0.1f);
                Debug.DrawRay(centerPosition, Vector2.down, Color.red, 1);
                if (checkRaycastWithScenario(hits)) { col1 = true; }

                hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 0.1f);
                if (checkRaycastWithScenario(hits)) { col2 = true; }
                Debug.DrawRay(leftPosition, -Vector2.up, Color.red, 1);

                hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 0.1f);
                if (checkRaycastWithScenario(hits)) { col3 = true; }
                Debug.DrawRay(rightPosition, -Vector2.up, Color.red, 1);

                if (col1 || col2 || col3) { isJumping = false; jumpForce = 5; deathbodyToCrash = null; }
            }
            else
            {
                isJumping = true;
            }

        }
        if (collision.gameObject.tag == "Body")
        {
            if (isJumping)
            {
                isJumping = false; 
                jumpForce = 10; 
                deathbodyToCrash = collision.gameObject;
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool(jumpHammerID, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
    }

    private bool checkRaycastWithScenario(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag == "Scenario") { return true; }
        }
        return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" ) {
            transform.position = startPosition;
        }
        else if (collision.gameObject.tag == "Lava")
        {
            GameObject deathBody = Instantiate(deathbody, new Vector3(transform.position.x, transform.position.y, 1f), transform.rotation);
            transform.position = startPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CheckPoint")
        {
            startPosition = transform.position;
        }
        else if (collision.tag == "NextLevel")
        {
            levelLoader.GetComponent<levelLoaderScript>().LoadNextLevel();
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);

        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
    }
}
