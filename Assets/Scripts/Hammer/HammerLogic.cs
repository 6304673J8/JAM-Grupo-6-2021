using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerLogic : MonoBehaviour
{
    public float speed = .3f;
    //public float distance = 5f;
    private Transform thrower;
    Rigidbody2D rb;

    float _travelledDistance;
    public bool isBack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        //thrower = GameObject.FindGameObjectWithTag("Finish").transform;
        thrower = GameObject.FindGameObjectWithTag("Player").transform;
        _travelledDistance = 0.0f;
        //thrower.gameObject.SetActive(true); // activating the object
    }

    public void Thrown()
    {
        Debug.Log("hammered");

        _travelledDistance = 0;
        isBack = false;
        enabled = true;
    }

    void Update()
    {
        //Vector2 dir = (Vector2)thrower.position - rb.position;
        //float route = speed * Time.deltaTime;
        _travelledDistance += Time.deltaTime;
        if (_travelledDistance >= 0.4f)
        {
            isBack = true;
        }

        if (!isBack)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); //hammer moves
            //_travelledDistance += route; //updates distance
            //isBack = _travelledDistance >= distance;
        }
        else
        {
            //transform.right = thrower.position - transform.position;
            transform.Translate(Vector2.right * -(speed * Time.deltaTime)); //moves object
            //_travelledDistance -= route;
            //enabled = _travelledDistance > 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Thrower")
        {
            speed = 0;
            //enabled = false;
        }
        if (other.tag == "Enemy")
        {
            isBack = true;
        }
    }

    /*private void OnDisable()
    {
        thrower.gameObject.SetActive(false); // deactivating the object
    }*/
}
