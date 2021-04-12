using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerLogic : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 5f;
    public Transform Hammer;

    private float _travelledDistance;
    public bool isBack = false;

    public void Thrown()
    {
        _travelledDistance = 0;
        isBack = false;
        enabled = true;
    }
    
    void Update()
    {
        float route = Time.deltaTime * speed;

        if (!isBack)
        {
            Hammer.Translate(Vector2.right * route); //hammer moves
            _travelledDistance += route; //updates distance
            isBack = _travelledDistance >= distance;
        }
        else
        {
            Hammer.Translate(Vector2.right * -route); //moves object
            _travelledDistance -= route;
            enabled = _travelledDistance > 0;
        }
    }

    private void OnEnable()
    {
        Hammer.gameObject.SetActive(true); // activating the object
    }
    private void OnDisable()
    {
        Hammer.gameObject.SetActive(false); // activating the object
    }
}
