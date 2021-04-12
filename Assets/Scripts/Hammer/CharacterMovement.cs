using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    /*Creación variables*/
    // Variables de componentes
    private Rigidbody2D rb2d;
    private BoxCollider2D box2D;

    // Variables para modificar el personaje
    public float speed = 2;
    public int jumpForce = 3;

    private int direction;
    private bool isJumping;

    //GEOH code
    public GameObject hammerPrefab;
    bool hasReturned;
    HammerLogic hammerLogic;

    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        rb2d = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();

        //GEOH code

        //hammerLogic = GameObject.FindGameObjectWithTag("Hammer").GetComponent<HammerLogic>();
        hammerLogic = GameObject.FindGameObjectWithTag("Respawn").GetComponent<HammerLogic>();
        hasReturned = hammerLogic.GetComponent<HammerLogic>().isBack;

    }

    void Update()
    {
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            direction = 1;
        }
        else if (Input.GetKey("a"))
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }

        if (Input.GetKey("space") && !isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        //GEOH code

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("ENTRA");
            hammerLogic.Thrown();
            if (hasReturned) {
                Debug.Log("t4esdted");

                GameObject hammerObject = Instantiate(hammerPrefab, transform.position + transform.right * 1, transform.rotation);
            }
        }

        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Scenario")
        {
            if (isJumping)
            {
                bool col1 = false;
                bool col2 = false;
                bool col3 = false;

                float center_x = (box2D.bounds.min.x + box2D.bounds.max.x) / 2;
                Vector2 centerPosition = new Vector2(center_x, box2D.bounds.min.y);
                Vector2 leftPosition = new Vector2(box2D.bounds.min.x, box2D.bounds.min.y);
                Vector2 rightPosition = new Vector2(box2D.bounds.max.x, box2D.bounds.min.y);

                RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, 2);
                if (checkRaycastWithScenario(hits)) { col1 = true; }

                hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 2);
                if (checkRaycastWithScenario(hits)) { col2 = true; }

                hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 2);
                if (checkRaycastWithScenario(hits)) { col3 = true; }

                if (col1 || col2 || col3) { isJumping = false; }
            }
        }
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
}
