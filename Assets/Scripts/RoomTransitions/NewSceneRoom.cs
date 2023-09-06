using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneRoom : MonoBehaviour
{
    public string newSceneName;
    public DialogueBase mainLockedDialogue;
    public DialogueBase secondaryLockedDialogue;
//Opening Dialogue must be an option
    public DialogueBase openingDialogue;
    private int interactionNumber;
    private int unlockedInteractionNumber;
    private bool locked = true;

    public Item gainableItem;
    public Item useableItem;
    public bool addOneItem;
    public bool useItem;
    private int itemsAdded;
    

    private void Start()
    {
        interactionNumber = 0;
        unlockedInteractionNumber = 0;
        itemsAdded = 0;
    }
    public void TriggerDialogue()
    {
        if (InventoryManager.instance.inventory.Contains(useableItem)| locked == false)
        { 
            locked = false;
            if (openingDialogue != null && unlockedInteractionNumber == 0)
            {
                DialogueManager.instance.EnqueueDialogue(openingDialogue);
            }
      
            else
            {
                GameManager.instance.NewScene(newSceneName);
            }
            unlockedInteractionNumber += 1;
        }
        else
        {
            LockedDialogue();
        }

        //Item Gaining and Loosing
        if (gainableItem != null)
        {  
            if (addOneItem == true && itemsAdded == 0 | addOneItem == false)
            {
                InventoryManager.instance.AddItem(gainableItem);
                itemsAdded += 1;
            }
          

        }
        if (useableItem != null && InventoryManager.instance.inventory.Contains(useableItem) && useItem == true)
        {
            InventoryManager.instance.RemoveItem(useableItem);
            if (secondaryLockedDialogue == null)
            {
                interactionNumber += 1;
            }

        }
    }

    private void LockedDialogue()
    {
        if (secondaryLockedDialogue != null)
        {
            if (interactionNumber == 0)
            {
                DialogueManager.instance.EnqueueDialogue(mainLockedDialogue);
                interactionNumber += 1;
            }
            if (interactionNumber > 0)
            {
                DialogueManager.instance.EnqueueDialogue(secondaryLockedDialogue);
            }
        }
        else
        {
            DialogueManager.instance.EnqueueDialogue(mainLockedDialogue);
        }

    }
}


