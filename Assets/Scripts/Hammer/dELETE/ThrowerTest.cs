using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerTest : MonoBehaviour
{
    public Transform thrower;
    public GameObject hammerPrefab;
    public GameObject throwerVisible;
    public float speed = .3f;
    float _travelledDistance;
    
    // Update is called once per frame
    void Update()
    {
        _travelledDistance += Time.deltaTime;

        if (Input.GetKeyDown("e") && GetComponent<AnimController>().hammer)
        {
            Debug.Log("e Pressed");
            Throw();
            throwerVisible.SetActive(false);
            GetComponent<AnimController>().hammer = false;
        }
    }

    void Throw()
    {
        GameObject hammer = Instantiate(hammerPrefab, thrower.position + transform.right * 1.2f, thrower.rotation);
    }
}
