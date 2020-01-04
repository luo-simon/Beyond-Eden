using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowAfterDialogue : MonoBehaviour
{
    public DialogueManager dialogue;

    public AudioSource audioSource;

    [SerializeField]
    private bool hasStarted = false;

    void Start()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hasStarted == true && dialogue.isPlaying == false)
        {
            StartCoroutine(ShowPanel());
            hasStarted = false;
        }
        if (dialogue.isPlaying) hasStarted = true;
    }

    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(0.5f);
        Image image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        audioSource.Play();
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
