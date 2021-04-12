using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public Transform thrower;
    public GameObject hammerPrefab;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("e Pressed");
            Throw();
        }
    }

    void Throw()
    {
        GameObject hammer = Instantiate(hammerPrefab, thrower.position + transform.right * 1.2f, thrower.rotation);
    }
}
