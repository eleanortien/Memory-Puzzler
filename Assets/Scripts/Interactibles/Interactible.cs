using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public DialogueBase mainDialogue;
    public DialogueBase secondaryDialogue;
    private int interactionNumber;

    public Item gainableItem;
    public Item useableItem;
    public bool selfDestruct;
    public bool gainOneDontDestruct;
    private int itemsAdded;
    public GameObject puzzleUI;
    public string puzzleType;
    

    private void Start()
    {
        interactionNumber = 0;
        itemsAdded = 0;
    }
    public void TriggerDialogue()
    {
        if (secondaryDialogue != null)
        {
            OneTimeDialogue();
        }
        else
        {
            if (mainDialogue != null)
            {
                DialogueManager.instance.EnqueueDialogue(mainDialogue);
            }
        }

        //Item Gaining and Loosing
        if (gainableItem != null && interactionNumber == 0)
        {
 
            if (gainOneDontDestruct == true && itemsAdded == 0 || gainOneDontDestruct == false)
            {
     
                InventoryManager.instance.AddItem(gainableItem);
                itemsAdded += 1;
                if (secondaryDialogue != null)
                {
                    interactionNumber += 1;
                }
            }
            if (selfDestruct == true)
            {
                Destroy(gameObject);
            }
            

        }
        if (useableItem != null && InventoryManager.instance.inventory.Contains(useableItem))
        {
            InventoryManager.instance.RemoveItem(useableItem);
            if (secondaryDialogue != null)
            {
                interactionNumber += 1;
            }

        }

        if (puzzleUI != null)
        {
            switch (puzzleType)
            {
                case "keypad":
                    puzzleUI.GetComponent<KeypadDisplay>().StartKeypadUI();
                    break;
            }
        }
    }

    private void OneTimeDialogue()
    {
        if (interactionNumber == 0)
        {
            DialogueManager.instance.EnqueueDialogue(mainDialogue);
            if (useableItem == null && gainableItem == null)
            {
                interactionNumber += 1;
            }
            
        }
        if (interactionNumber > 0)
        {
            DialogueManager.instance.EnqueueDialogue(secondaryDialogue);
        }

    }
}
