using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1000f;
    public void LoadNextLevel()
    {
        transition.SetTrigger("Start");
        Invoke("loadTheScene", 1);
    }
    void loadTheScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }
}
