using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public Animator creditsAnim;

    public string targetScene;

    void Start()
    {
        creditsAnim = transform.GetChild(3).GetComponent<Animator>();
    }

    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        FadeToLevel();
    }

    void FadeToLevel()
    {
        anim.SetTrigger("FadeOut");
    }

    void OnFadeComplete()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void ShowCredits()
    {
        creditsAnim.SetTrigger("Move");
    }

    public void QuitGame()
    {
        Debug.Log("Game has QUIT");
        Application.Quit();
    }
}
