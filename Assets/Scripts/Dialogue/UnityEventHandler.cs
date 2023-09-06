using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public DialogueBase dialogue;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();
        if (eventHandler.GetPersistentEventCount() == 0)
        {
            DialogueManager.instance.CloseOptions();
        }
        if (dialogue != null)
        {
            DialogueManager.instance.EnqueueDialogue(dialogue);
        }
        
    }

}
   
