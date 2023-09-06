using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBehaviour : ScriptableObject
{
    public DialogueBase responseDialogue;
    public Item gainableItem;
    public Item useableItem;
    public string newSceneName;
    public string interactedObjectName;
    public Sprite newSprite;

    public void DialogueEvent()
    {
        DialogueManager.instance.CloseOptions();
        DialogueManager.instance.EnqueueDialogue(responseDialogue);

    }

    public void CutsceneEvent()
    {

    }

    public void NewArea()
    {
        DialogueManager.instance.CloseOptions();
        GameManager.instance.NewScene(newSceneName);
    }

    public void GainItem()
    {
        InventoryManager.instance.AddItem(gainableItem);
    }

    public void LooseItem()
    {
        InventoryManager.instance.RemoveItem(useableItem);
    }

    public void SaveInfo()
    {
        GameManager.instance.Save();
    }

    public void ChangeSprite()
    {
        GameObject interactedObject = GameObject.Find(interactedObjectName);
        SpriteRenderer spriteRenderer = interactedObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
    
    
}
