using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI textDisplay;

    public string[] sentences;

    [HideInInspector]
    public int index;

    public float typingSpeed = 0.005f;

    private GameObject continueButton;
    private Button btn;

    private GameObject dialogueSystem;

    public GameObject dialogueSystemPrefab;
    public Transform canvas;

    private PlayerController playerController;

    private GameObject speechBubble;

    public bool isPlaying = false;

    void Start()
    {
        if (transform.childCount > 0){
            speechBubble = transform.GetChild(0).gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        index = 0;
        if (other.CompareTag("Player"))
        {
            speechBubble.GetComponent<Animator>().SetTrigger("Appear");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isPlaying)
        {
            if (/*GetComponent<ChoiceManager>().chosenPostText == "" || */!PlayerPrefs.HasKey(gameObject.name + "chosenPostText"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerController = other.GetComponent<PlayerController>();
                    if (GetComponent<ChoiceManager>())
                    {
                        GetComponent<ChoiceManager>().playerController = other.GetComponent<PlayerController>();
                    }

                    playerController.canMove = false;

                    dialogueSystem = Instantiate(dialogueSystemPrefab, canvas);

                    textDisplay = dialogueSystem.GetComponentInChildren<TextMeshProUGUI>();
                    textDisplay.text = "";

                    continueButton = dialogueSystem.transform.GetChild(2).gameObject;

                    btn = continueButton.GetComponent<Button>();
                    btn.onClick.AddListener(NextSentence);

                    StartCoroutine(Type());

                    isPlaying = true;
                }
            } else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (dialogueSystem)
                    {
                        dialogueSystem.SetActive(true);
                    } else
                    {
                        dialogueSystem = Instantiate(dialogueSystemPrefab, canvas);
                        textDisplay = dialogueSystem.GetComponentInChildren<TextMeshProUGUI>();
                        textDisplay.text = "";
                        continueButton = dialogueSystem.transform.GetChild(2).gameObject;
                        playerController = other.GetComponent<PlayerController>();
                    }

                    if (/*GetComponent<ChoiceManager>().chosenRepeatText*/PlayerPrefs.HasKey(gameObject.name + "chosenRepeatText"))
                    {
                        StartCoroutine(Type(/*GetComponent<ChoiceManager>().chosenPostText*/PlayerPrefs.GetString(gameObject.name + "chosenPostText")));
                    }
                    else
                    {
                        StartCoroutine(Type("... ... ..."));
                    }


                    continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    continueButton.GetComponent<Button>().onClick.AddListener(Close);

                    playerController.canMove = false;
                    isPlaying = true;
                }
            }
        }

    }
    
    void Update()
    {
        if (dialogueSystem)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
            if (/*GetComponent<ChoiceManager>().chosenPostText != ""*/PlayerPrefs.HasKey(gameObject.name + "chosenPostText")) 
            {
                if (textDisplay.text == PlayerPrefs.GetString(gameObject.name + "chosenPostText") || textDisplay.text == "... ... ...")
                {
                    continueButton.SetActive(true);
                }
            }
        }
    }

    IEnumerator Type(string postText = "")
    {
        if(postText == "")
        {
            foreach (char letter in sentences[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        } else
        {
            foreach (char letter in postText)
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        
    }

    public void NextSentence()
    {
        playSound();

        continueButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else
        {
            Close();

            if (GetComponent<ChoiceManager>())
            {
                GetComponent<ChoiceManager>().initiated = true;
            }
        }
    }

    public void Close()
    {
        playSound();
        textDisplay.text = "";
        continueButton.SetActive(false);
        dialogueSystem.SetActive(false);
        playerController.canMove = true;
        isPlaying = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speechBubble.GetComponent<Animator>().SetTrigger("Exit");
    }

    void playSound()
    {
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
