using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemInteractible : MonoBehaviour
{
    public DialogueBase useItemOption;
    public DialogueBase noItemDialogue;
    public DialogueBase itemUsedDialogue;
    private bool usedItem = false;

    public bool itemIsUsed;
    public bool itemGainable = true;
    private int interactionNumber = 0;

    public Item gainableItem = null;
    public Item itemRequired;

    public void TriggerDialogue()
    {
      if (itemGainable)
        {
            if (InventoryManager.instance.inventory.Contains(itemRequired) && InventoryManager.instance.inventory.Contains(gainableItem) == false)
            {
                DialogueManager.instance.EnqueueDialogue(useItemOption);
                usedItem = true;
                if (!itemIsUsed)
                {
                    interactionNumber += 1;
                }

            }
            else
            {
                if (!usedItem)
                {
                    DialogueManager.instance.EnqueueDialogue(noItemDialogue);
                    
                }
                else
                {
                    DialogueManager.instance.EnqueueDialogue(itemUsedDialogue);
                }
            }
      }
      else
        {
            if (InventoryManager.instance.inventory.Contains(itemRequired) && interactionNumber == 0)
            {
                DialogueManager.instance.EnqueueDialogue(useItemOption);
                if (!itemIsUsed)
                {
                    interactionNumber += 1;
                }

            }
            else
            {
                if (interactionNumber == 0)
                {
                    DialogueManager.instance.EnqueueDialogue(noItemDialogue);
                   
                }
                else
                {
                    DialogueManager.instance.EnqueueDialogue(itemUsedDialogue);
                }
            }
        }
    }


}
