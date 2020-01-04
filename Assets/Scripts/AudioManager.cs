using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip clip1;
    public AudioClip pathClip;
    public AudioClip moralEndClip;
    public AudioClip immoralEndClip;

    public bool done = false;

    public string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Audio Manager").Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MountainPath" && currentScene != "MountainPath")
        {
            FadeOut(pathClip);
            if (done)
            {
                currentScene = "MountainPath";
                done = false;
            }
        }
        if (SceneManager.GetActiveScene().name == "End Moral" && currentScene != "End Moral")
        {
            FadeOut(moralEndClip);
            if (done)
            {
                currentScene = "End Moral";
                done = false;
            }
        }
        if (SceneManager.GetActiveScene().name == "Street Psychopath End" && currentScene != "Street Psychopath End")
        {
            FadeOut(immoralEndClip);
            if (done)
            {
                currentScene = "Street Psychopath End";
                done = false;
            }
        }
    }

     void FadeOut(AudioClip clip)
    {
        if(source.volume > 0.1)
        {
            source.volume -= 0.5f * Time.deltaTime;
        } else
        {
            source.volume = 0.4f;
            source.clip = clip;
            source.Play();
            done = true;
        }
    }

}
