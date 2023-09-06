using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "NewDialogueOption", menuName = "DialogueOptions")]
public class DialogueOptions : DialogueBase
{
    [TextArea(2, 10)]
    public string questionText;

    [System.Serializable]
    public class Options
    {
        public string buttonName;
        public UnityEvent optionEvent;
    }
    public Options[] optionsInfo;
    public DialogueBase nextDialogue;
}