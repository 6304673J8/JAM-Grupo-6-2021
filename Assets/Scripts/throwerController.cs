using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwerController : MonoBehaviour
{
    private int direction;
    public float xDistance;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.parent.GetComponent<PlayerController>().direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.parent.GetComponent<PlayerController>().direction;
        if (direction != 0)
        {
            float posX = transform.parent.GetComponent<Transform>().position.x;
            float posY = transform.parent.GetComponent<Transform>().position.y;

            if (direction == 1)
            {
                transform.position = new Vector3(xDistance + posX, 0.2f + posY, 0);
            }
            else
            {
                transform.position = new Vector3(-xDistance + posX, 0.2f + posY, 0);
            }
        }
    }
}
