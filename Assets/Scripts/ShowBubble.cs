using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBubble : MonoBehaviour
{
    private GameObject speechBubble;

    void Start()
    {
        speechBubble = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            speechBubble.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        speechBubble.GetComponent<Animator>().SetTrigger("Exit");
    }
}
