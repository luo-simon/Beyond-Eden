using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceQuit : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public bool initiated = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.isPlaying == true)
        {
            initiated = true;
        }

        if (initiated)
        {
            if (dialogueManager.isPlaying == false) {
                Application.Quit();
                Debug.Log("QUIT");
            }
        }
    }
}
