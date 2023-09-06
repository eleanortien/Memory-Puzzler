using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogues")]

public class DialogueBase : ScriptableObject 
{

    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;
        public EmotionType characterEmotion;
        public bool hasImage;
        public Sprite cutsceneImage;
        [TextArea(4, 8)]
        public string dialogueLines;
        public void ChangeEmotion ()
        {
            character.Emotion = characterEmotion;
        }
    }

    [Header("Insert Dialogue Info Below")]
    public Info[] dialogueInfo;
    public DialogueBase nextCommonDialogue;

}
    

