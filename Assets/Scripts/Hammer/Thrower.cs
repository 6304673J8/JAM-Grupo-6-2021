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

    // Update is called once per frame
    void Update()
    {
        _travelledDistance += Time.deltaTime;

        if (Input.GetKeyDown("e") && GetComponent<PlayerController>().hammer)
        {
            Debug.Log("e Pressed");
            Throw();
            throwerVisible.SetActive(false);
            GetComponent<PlayerController>().hammer = false;
        }
    }

    void Throw()
    {
        GameObject hammer = Instantiate(hammerPrefab, thrower.position, thrower.rotation);
    }
}
