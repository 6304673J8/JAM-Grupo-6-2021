using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject sceneLoader;
    public Slider soundSlider;
    public AudioMixer audioMixer;
    private void Start()
    {
    }
    public void PlayGame()
    {
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void setMusic(float vol)
    {
        audioMixer.SetFloat("music", vol);
    }
    public void setSFX(float vol)
    {
        audioMixer.SetFloat("sfx", vol);
    }
}
