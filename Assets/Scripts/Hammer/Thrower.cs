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

    bool attacked = false;
    private void Start()
    {
        attackHammerID = Animator.StringToHash("ThrownH");
        noHammerID = Animator.StringToHash("isBack");

    }
    // Update is called once per frame
    void Update()
    {
        attacked = false;

        _travelledDistance += Time.deltaTime;

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
