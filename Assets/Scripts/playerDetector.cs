using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.parent.GetComponent<SlimeController>().direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.parent.GetComponent<SlimeController>().direction;
        float posX = transform.parent.GetComponent<Transform>().position.x;
        float posY = transform.parent.GetComponent<Transform>().position.y;
        if (direction == 1)
        {
            transform.position = new Vector3(2f + posX, 0.5f + posY, 0);
        }
        else
        {
            transform.position = new Vector3(-2f + posX, 0.5f + posY, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent.GetComponent<SlimeController>().playerDetected = true;
            transform.parent.GetComponent<SlimeController>().totalTime = 0;
        }
    }
}
