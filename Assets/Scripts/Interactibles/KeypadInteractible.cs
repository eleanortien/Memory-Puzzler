using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadInteractible : MonoBehaviour
{
    public DialogueBase puzzleSolvedDialogue;
    public bool itemGainable = true;

    public Item gainableItem = null;
    public GameObject puzzleUI;
    public string puzzleType;

    public void TriggerDialogue()
    {
        if (puzzleUI != null && InventoryManager.instance.inventory.Contains(gainableItem) == false)
        {
            switch (puzzleType)
            {
                case "keypad":
                    puzzleUI.GetComponent<KeypadDisplay>().StartKeypadUI();
                    break;
            }
        }

        else if (puzzleSolvedDialogue != null && InventoryManager.instance.inventory.Contains(gainableItem))
        {
            DialogueManager.instance.EnqueueDialogue(puzzleSolvedDialogue);
        }
    }
}
