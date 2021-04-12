using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownHammer : MonoBehaviour
{
    /*public Transform thrower;
    public GameObject hammerPrefab;

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Thrown();
        }
    }

    void Thrown()
    {
        Instantiate(hammerPrefab, thrower.position, thrower.rotation);
    }*/
    public float speed = 1f;
    public float distance = 5f;
    public Transform thrower;

    private float _travelledDistance;
    public bool isBack;
    public void Thrown()
    {
        Debug.Log("hammered");

        _travelledDistance = 0;
        isBack = false;
        enabled = true;
    }

    void Update()
    {
        float route = Time.deltaTime * speed;
        if (Input.GetKeyDown("e"))
        {
            Thrown();

            Instantiate(this, thrower.position, thrower.rotation);
        }
        if (!isBack)
        {
            thrower.Translate(Vector2.right * route); //hammer moves
            _travelledDistance += route; //updates distance
            isBack = _travelledDistance >= distance;
        }
        else
        {
            thrower.Translate(Vector2.right * -route); //moves object
            _travelledDistance -= route;
            enabled = _travelledDistance > 0;
        }
    }
    private void OnEnable()
    {
        thrower.gameObject.SetActive(true); // activating the object
    }
    private void OnDisable()
    {
        thrower.gameObject.SetActive(false); // deactivating the object
    }
}
