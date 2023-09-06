using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractible : MonoBehaviour
{
    public DialogueBase lockedDialogue;
    public DialogueBase unlockedDialogue;
    private SpriteRenderer spriteRenderer;
    public Sprite unlockedSprite;
    public bool clearFloorItems;
    public List<Item> removeItems;
   
  

    public void TriggerDialogue()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == unlockedSprite)
        {
            if (clearFloorItems)
            {
                ClearFloorItems(removeItems);
            }
            DialogueManager.instance.EnqueueDialogue(unlockedDialogue);
          
        }
        else
        {
            DialogueManager.instance.EnqueueDialogue(lockedDialogue);
        }

       
    }

    public void ClearFloorItems (List<Item> itemList)
    {
        foreach (Item item in itemList)
        {
            InventoryManager.instance.RemoveItem(item);
        }
    }
}
