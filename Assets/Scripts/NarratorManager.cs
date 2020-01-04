using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorManager : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    public bool isPlaying;
    private float audioClipLength;
    private PlayerController player;
    public GameObject cutsceneBars;
    [SerializeField]
    private bool cutsceneHasStarted = false;

    [SerializeField]
    private Collider2D col;
    public bool autoPlay;
    public float slowMotionTimeScale = 0.2f;
    public float delayBeforeSlowMotion;
    public float slowDownRate = 0.2f;  

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        audioClipLength = audioClip.length;

        if (!autoPlay) col = GetComponentInChildren<Collider2D>();
    }

    void Update()
    {
        isPlaying = audioSource.isPlaying;
        if (autoPlay && !isPlaying)
        {
            StartCoroutine(PlayAudio());
            autoPlay = false;
        }

        if (isPlaying)
        {
            StartCoroutine(SlowTime());
        }
        else
        {
            Time.timeScale = 1f;
            if (cutsceneHasStarted)
            {
                StartCoroutine(RemoveCutsceneBars());
            }
        }
    }

    public void CollisionDetected()
    {
        Debug.Log("Trigger");
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        cutsceneBars.SetActive(true);
        yield return new WaitForSeconds(delayBeforeSlowMotion);
        Debug.Log("Audio will now play...");
        audioSource.Play();
    }

    IEnumerator SlowTime()
    {
        //Debug.Log("Timescale: " + Time.timeScale);
        if (Time.timeScale > slowMotionTimeScale)
        {
            
            for (int i = 0; Time.timeScale > slowMotionTimeScale; i++)
            {
                yield return new WaitForSeconds(slowDownRate);
                Time.timeScale -= 0.005f;
            }
        }
        player.canMove = false;
        cutsceneHasStarted = true;
    }

    IEnumerator RemoveCutsceneBars()
    {
        cutsceneHasStarted = false;
        Animator anim = cutsceneBars.GetComponent<Animator>();
        anim.SetTrigger("MoveOut");
        yield return new WaitForSeconds(1);
        cutsceneBars.SetActive(false);
        player.canMove = true;
        col.enabled = false;
    }
}
