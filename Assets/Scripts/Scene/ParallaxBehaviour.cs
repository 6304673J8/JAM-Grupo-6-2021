using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    private float length, startingPoint;
    public GameObject cameraObject;
    public float parallaxEffect;

    void Start()
    {
        startingPoint = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cameraObject.transform.position.x * (1 - parallaxEffect));
        float distance = (cameraObject.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startingPoint + distance, transform.position.y, transform.position.z);

        if (temp > startingPoint + length) {
            startingPoint += length;
        }
        else if (temp < startingPoint - length)
        {
            startingPoint -= length;
        }
    }
}
