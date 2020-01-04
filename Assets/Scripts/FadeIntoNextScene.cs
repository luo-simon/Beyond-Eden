using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class FadeIntoNextScene : MonoBehaviour
{
    public Light2D lightSource;

    public float time = 3f;

    [SerializeField]
    private string SceneToLoad;

    [SerializeField]
    private bool startFade;

    void Start()
    {
        lightSource = GetComponentInParent<Light2D>();
    }

    void Update()
    {
        if(startFade) lightSource.color -= (Color.white / time) * Time.deltaTime;
    }

    void OnTriggerEnter2D()
    {
        lightSource.GetComponent<ChangeLightColour>().enabled = false;
        startFade = true;
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        DecideScene();
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneToLoad);
    }

    void DecideScene()
    {
        if (PlayerPrefs.GetInt("PlayerPoints") < 0) SceneToLoad = "End Expected";
        if (PlayerPrefs.GetInt("PlayerPoints") > 0) SceneToLoad = "End Moral";
        if (PlayerPrefs.GetInt("PlayerPoints") <= -35) SceneToLoad = "End Psychopath";
    }
}
