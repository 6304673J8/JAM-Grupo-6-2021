using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1000f;
    private string sceneToLoad;
    public void LoadNextLevel()
    {
        transition.SetTrigger("Start");
        Invoke("loadTheScene", 1);
    }
    void loadTheScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void loadTheSceneWith(string scene)
    {
        transition.SetTrigger("Start");
        sceneToLoad = scene;
        Invoke("loadThisScene", 1);
    }
    void loadThisScene()
    {
        SceneManager.LoadScene(sceneToLoad);
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
