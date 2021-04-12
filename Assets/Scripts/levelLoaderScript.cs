using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1000f;
    private string sceneToLoad;
    public void LoadNextLevel(string loadScene)
    {
        transition.SetTrigger("Start");
        sceneToLoad = loadScene;
        Invoke("loadTheScene", 1);
    }
    void loadTheScene()
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
