using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    //Punctuation Pause
    private readonly List<char> punctuationCharacters = new List<char>
    {
        '.',
        ',',
        '?',
        '!'
    };



    //Allow references from any script
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Fix this " + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }



    public GameObject dialogueBox;
    public Image dialoguePortraitLeft;
    public Image dialoguePortraitRight;
    public Text dialogueName;
    public Text dialogueText;
    private bool isPortraitLeft;
    private DialogueBase currentDialogue;

    public Image cutsceneImageBox;

    private bool isCurrentlyTyping;
    private string completeText;
    public float textScrollDelay = 0.01f;

    public Animator leftAnimator;
    public Animator rightAnimator;
    private Sprite lastSprite;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();

    //Dialogue Options Variables
    private bool isDialogueOptions;
    public GameObject dialogueOptionsUI;
    public bool inDialogue;
    public GameObject[] optionButtons;
    private int optionsAmount;
    public Text questionText;
    private UnityEventHandler mainEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        
        dialogueBox.SetActive(false);
        dialogueOptionsUI.SetActive(false);
        cutsceneImageBox.enabled = false;
        
    }

    public void EnqueueDialogue (DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue = true;
        //Make Player Immovable
        PlayerMovement state = FindObjectOfType<PlayerMovement>();
        state.currentState = PlayerState.interact;
        //Prepare dialogue
        dialogueInfo.Clear(); //clear previous dialogue information
        currentDialogue = db;
        if (db is DialogueOptions)
        {
            OptionParser(db);
        }
        else
        {
            isDialogueOptions = false;
        }
        dialogueBox.SetActive(true);

        //Enqueue information FIFO
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        DequeueDialogue(db);
    }
    public void DequeueDialogue (DialogueBase db)
    {
       
        if (isCurrentlyTyping == true)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndDialogue();
            if (db is DialogueBase && db.nextCommonDialogue != null)
            {
                EnqueueDialogue(db.nextCommonDialogue);
            }
            return;
        }

        FinishTalking();
        //Dequeue Information
        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.dialogueLines;
        //Update New Dialogue Information and Change UI
        dialogueName.text = info.character.characterName;
        dialogueText.text = info.dialogueLines;
        //Change Emotion
        info.ChangeEmotion();
        //Portrait display left vs right side
        isPortraitLeft = info.character.isPortraitLeft;
        lastSprite = info.character.Portrait;

        if (info.character.characterAOC != null)
        {
            switch (isPortraitLeft)
            {
                case true:
                    dialoguePortraitLeft.enabled = true;
                    dialoguePortraitRight.enabled = false;
                    leftAnimator.enabled = true;
                    rightAnimator.enabled = false;
                    leftAnimator.runtimeAnimatorController = info.character.characterAOC;
                    leftAnimator.SetBool("isTalking", true);
                    leftAnimator.Play(info.characterEmotion.ToString());
                    break;

                case false:
                    dialoguePortraitRight.enabled = true;
                    dialoguePortraitLeft.enabled = false;
                    rightAnimator.enabled = true;
                    leftAnimator.enabled = false;
                    rightAnimator.runtimeAnimatorController = info.character.characterAOC;
                    rightAnimator.SetBool("isTalking", true);
                    rightAnimator.Play(info.characterEmotion.ToString());
                    break;
            }
        }

        else
        {
            switch (isPortraitLeft)
            {
                case true:
                    dialoguePortraitLeft.sprite = info.character.Portrait;
                    dialoguePortraitLeft.enabled = true;
                    dialoguePortraitRight.enabled = false;
                    break;

                case false:
                    dialoguePortraitRight.sprite = info.character.Portrait;
                    dialoguePortraitRight.enabled = true;
                    dialoguePortraitLeft.enabled = false;
                    break;

            }
        }
        
        //Add cutscene image
        if (info.hasImage == true)
        {
            cutsceneImageBox.GetComponent<Image>().sprite = info.cutsceneImage;
            cutsceneImageBox.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(300, 300); 
            cutsceneImageBox.enabled = true;
        }
        else
        {
            cutsceneImageBox.enabled = false;
        }

        //Text Scrolling Activate
        StopAllCoroutines();
        dialogueText.text = ""; //empty text for typing (refresh)
        StartCoroutine(TypeText(info));
    }
    
   private bool CheckPunctuation (char character)
    {
        if (punctuationCharacters.Contains (character))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Text Scrolling
    IEnumerator TypeText (DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        
        foreach (char letter in info.dialogueLines.ToCharArray())
        {
            yield return new WaitForSeconds(textScrollDelay);
            dialogueText.text += letter;
           if (info.character.voice != null)
            {
                AudioManager.instance.PlayClip(info.character.voice);
            }
           if (CheckPunctuation (letter))
            {
                yield return new WaitForSeconds(0.2f);
            }

        }
        isCurrentlyTyping = false;
    }
    private void CompleteText ()
    {
        
        dialogueText.text = completeText;
    }
    private void FinishTalking ()
    {
        switch (isPortraitLeft)
        {
            case true:
                leftAnimator.SetBool("isTalking", false);
                leftAnimator.enabled = false;
                dialoguePortraitLeft.sprite = lastSprite;
                break;

            case false:
                rightAnimator.SetBool("isTalking", false);
                rightAnimator.enabled = false;
                dialoguePortraitRight.sprite = lastSprite;
                break;
        }
        
    }
    public void AdvanceDialogue()
    {
        DequeueDialogue(currentDialogue);
    }
    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        OptionsLogic();
    }
    
   private void OptionParser (DialogueBase db)
    {
        isDialogueOptions = true;
        DialogueOptions dialogueOptions = db as DialogueOptions;
        optionsAmount = dialogueOptions.optionsInfo.Length;
        questionText.text = dialogueOptions.questionText;
        for (int i = 0; i < optionsAmount; i++)
        {
            optionButtons[i].SetActive(true);
            optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonName;
            mainEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
            mainEventHandler.eventHandler = dialogueOptions.optionsInfo[i].optionEvent;
            if (dialogueOptions.nextDialogue != null)
            {
                mainEventHandler.dialogue = dialogueOptions.nextDialogue;
            }
            else
            {
                mainEventHandler.dialogue = null;
            }
        }
    }
    private void OptionsLogic ()
    {
        if (isDialogueOptions == true)
        {
            PlayerMovement state = FindObjectOfType<PlayerMovement>();
            state.currentState = PlayerState.interact;
            dialogueOptionsUI.SetActive(true);
           
        }
        else
        {
            inDialogue = false;
            PlayerMovement state = FindObjectOfType<PlayerMovement>();
            state.currentState = PlayerState.walk;
            cutsceneImageBox.enabled = false;
        }
    }
    public void CloseOptions ()
    {
        for (int i = 0; i < optionsAmount; i++)
        {    
            optionButtons[i].SetActive(false);
        }
        dialogueOptionsUI.SetActive(false);
        inDialogue = false;
        PlayerMovement state = FindObjectOfType<PlayerMovement>();
        state.currentState = PlayerState.walk;

    }
}

