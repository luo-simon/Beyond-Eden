using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;

    public Transform canvas;

    public GameObject choiceSystemPrefab;
    private GameObject choiceSystem;

    public string choiceOneText = "Choice 1";
    public int choiceOnePoints;
    private GameObject choiceOneObject;

    public string choiceTwoText = "Choice 2";
    public int choiceTwoPoints;
    private GameObject choiceTwoObject;

    public string choiceThreeText = "Choice 3";
    public int choiceThreePoints;
    private GameObject choiceThreeObject;

    private Button closeButton;

    public bool initiated = false;

    public string postText1;
    public string postText2;
    public string postText3;
    [HideInInspector]
    public string chosenPostText = "";

    public bool rememberChoice = true;

    public bool repeatPostText1 = true;
    public bool repeatPostText2 = true;
    public bool repeatPostText3 = true;
    [HideInInspector]
    public bool chosenRepeatText;

    public Sprite postSprite1;
    public Sprite postSprite2;
    public Sprite postSprite3;
    [HideInInspector]
    public Sprite chosenSprite;

    private GameObject dialogueBox;
    private GameObject continueBtn;

    private TextMeshProUGUI textDisplay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (initiated && choiceSystemPrefab)
        {
            GetComponent<DialogueManager>().isPlaying = true;
            playerController.canMove = false;

            choiceSystem = Instantiate(choiceSystemPrefab, canvas);

            choiceOneObject = choiceSystem.transform.GetChild(1).gameObject;
            choiceTwoObject = choiceSystem.transform.GetChild(2).gameObject;
            choiceThreeObject = choiceSystem.transform.GetChild(3).gameObject;

            dialogueBox = choiceSystem.transform.GetChild(5).gameObject;
            continueBtn = choiceSystem.transform.GetChild(6).gameObject;

            textDisplay = dialogueBox.GetComponent<TextMeshProUGUI>();

            choiceOneObject.GetComponent<TextMeshProUGUI>().text = "> " + choiceOneText + " <";
            choiceTwoObject.GetComponent<TextMeshProUGUI>().text = "> " + choiceTwoText + " <";
            choiceThreeObject.GetComponent<TextMeshProUGUI>().text = "> " + choiceThreeText + " <";

            continueBtn.GetComponent<Button>().onClick.AddListener(Close);

            choiceOneObject.GetComponent<Button>().onClick.AddListener(delegate { buttonOnClick(choiceOnePoints, postText1, repeatPostText1, postSprite1); });
            choiceTwoObject.GetComponent<Button>().onClick.AddListener(delegate { buttonOnClick(choiceTwoPoints, postText2, repeatPostText2, postSprite2); });
            choiceThreeObject.GetComponent<Button>().onClick.AddListener(delegate { buttonOnClick(choiceThreePoints, postText3, repeatPostText3, postSprite3); });

            closeButton = choiceSystem.transform.GetChild(4).gameObject.GetComponent<Button>();
            closeButton.onClick.AddListener(Close);

            initiated = false;
        }


        void buttonOnClick(int points, string text, bool repeat, Sprite sprite)
        {
            choiceOneObject.SetActive(false);
            choiceTwoObject.SetActive(false);
            choiceThreeObject.SetActive(false);
            closeButton.gameObject.SetActive(false);

            dialogueBox.SetActive(true);
            
            textDisplay.text = "";

            if (rememberChoice)
            {
                //chosenPostText = text;

                PlayerPrefs.SetString(gameObject.name + "chosenPostText", text);
            }

            StartCoroutine(Type(text));

            GameObject.FindWithTag("Player").GetComponent<PointsManager>().ChangePoints(points);

            //chosenRepeatText = repeat;
            if (repeat)
            {
                PlayerPrefs.SetInt(gameObject.name + "chosenRepeatText", 1);
            }

            if (sprite)
            {
                chosenSprite = sprite;
            }
        }

        IEnumerator Type(string text)
        {
            foreach (char letter in text.ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(0.005f);
            }
            continueBtn.SetActive(true);

        }



        void Close()
        {
            if (chosenSprite)
            {
                GetComponent<Animator>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = chosenSprite;
            }
            

            GetComponent<DialogueManager>().index = 0;
            GetComponent<DialogueManager>().isPlaying = false;

            playerController.canMove = true;
            choiceSystem.SetActive(false);
        }

    }
}
