using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Profile", menuName = "Character Profile")]
public class CharacterProfile : ScriptableObject
{
    public string characterName;
    public AudioClip voice;

    //Portrait
    private Sprite portrait;
    public Sprite Portrait
    {
        get
        {
            SetEmotionType(Emotion);
            return portrait;
        }
    }
    public bool isPortraitLeft;
    public AnimatorOverrideController characterAOC;

    [System.Serializable]
    public class EmotionPortraits
    {
        public Sprite standard;
        public Sprite happy;
        public Sprite annoyed;
        public Sprite reluctant;
        public Sprite embarrassed;
        public Sprite scared;
        public Sprite shocked;
        public Sprite sad;
        public Sprite serious;
        public Sprite inquisitive;
        public Sprite confused;
    }
    public EmotionPortraits emotionPortraits;
    
    public EmotionType Emotion { get; set; }
    public void SetEmotionType(EmotionType newEmotion)
    {
        Emotion = newEmotion;
        switch (Emotion)
        {
            case EmotionType.Standard:
                portrait = emotionPortraits.standard;
                break;

            case EmotionType.Happy:
                portrait = emotionPortraits.happy;
                break;

            case EmotionType.Annoyed:
                portrait = emotionPortraits.annoyed;
                break;

            case EmotionType.Reluctant:
                portrait = emotionPortraits.reluctant;
                break;

            case EmotionType.Embarrassed:
                portrait = emotionPortraits.embarrassed;
                break;

            case EmotionType.Scared:
                portrait = emotionPortraits.scared;
                break;

            case EmotionType.Shocked:
                portrait = emotionPortraits.shocked;
                break;

            case EmotionType.Sad:
                portrait = emotionPortraits.sad;
                break;

            case EmotionType.Serious:
                portrait = emotionPortraits.serious;
                break;

            case EmotionType.Inquisitive:
                portrait = emotionPortraits.inquisitive;
                break;

            case EmotionType.Confused:
                portrait = emotionPortraits.confused;
                break;
        }
    }
}
    public enum EmotionType
    {
        Standard,
        Happy,
        Annoyed,
        Reluctant,
        Embarrassed,
        Scared,
        Shocked,
        Sad,
        Serious,
        Inquisitive,
        Confused
    }
    
    
    

