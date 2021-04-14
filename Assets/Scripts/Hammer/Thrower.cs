using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public Transform thrower;
    public GameObject hammerPrefab;
    public GameObject throwerVisible;
    public float speed = .3f;
    float _travelledDistance;

    //animation
    public Animator animator;
    private int attackHammerID;
    private int noHammerID;
    private int runNoHammerID;
    private int jumpNoHammerID;

    bool attacked = false;
    private void Start()
    {
        attackHammerID = Animator.StringToHash("ThrownH");
        noHammerID = Animator.StringToHash("isBack");
        runNoHammerID = Animator.StringToHash("isNoHammerMoving");
        jumpNoHammerID = Animator.StringToHash("isNoHammerJumping");
    }
    // Update is called once per frame
    void Update()
    {
        attacked = false;
        bool hasJumped = false;

        _travelledDistance += Time.deltaTime;
        bool isMoving = false;

        if (Input.GetKeyDown("e") && GetComponent<PlayerController>().hammer)
        {
            animator.SetTrigger(attackHammerID);
            Invoke("Throw", .6f);
            //Throw();
            attacked = true;
            throwerVisible.SetActive(false);
            GetComponent<PlayerController>().hammer = false;
            animator.SetBool(noHammerID, false);
        }
        
        if (Input.GetKey("space"))
        {
            hasJumped = true;
        }

        if (Input.GetKey("d"))
        {
            isMoving = true;
        }
        else if (Input.GetKey("a"))
        {
            isMoving = true;
        }
        animator.SetBool(runNoHammerID, isMoving);
        animator.SetBool(jumpNoHammerID, hasJumped);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hammer")
        {
            Debug.Log("Test");
            animator.SetBool(noHammerID, true);
            //animator.SetBool(noHammerID, true);

        }
    }

    void Throw()
    {
        GameObject hammer = Instantiate(hammerPrefab, thrower.position, thrower.rotation);
    }
}
