using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CutsceneManager : MonoBehaviour
{

    private TextMeshProUGUI textObj;

    public string[] sentences;

    public Sprite[] images;

    private RawImage imgDisplay;

    public int changeImgEvery = 2;

    [HideInInspector]
    public int index = 0;

    public float typingSpeed = 0.02f;

    public Animator anim;

    void Start()
    {
        textObj = GetComponentInChildren<TextMeshProUGUI>();
        imgDisplay = transform.parent.GetComponentInChildren<RawImage>();
        UpdateImg();
        textObj.text = "";

        StartCoroutine(Type());
    }

    void Update()
    {
        if (textObj.text == sentences[index])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextSentence();
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                textObj.text = sentences[index];
            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textObj.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            UpdateImg();
            textObj.text = "";
            StartCoroutine(Type());
        }
        else
        {
            Debug.Log("Transitioning to next scene...");
            //TRIGGER TRANSITION INTO FIRST GAMEPLAY SCENE
            anim.SetTrigger("SceneChange");
        }
    }

    public void UpdateImg()
    {
        if (index % changeImgEvery == 0)
        {
            if(index != 0)
            {
                anim.SetTrigger("FadeTrigger");
            }
        }
    }

    public void OnImgFadeOut()
    {
        imgDisplay.texture = images[index / changeImgEvery].texture;
    }

    public void OnSceneChange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
