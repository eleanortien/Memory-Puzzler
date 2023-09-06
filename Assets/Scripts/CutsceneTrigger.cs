using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{

    public DialogueBase cutsceneDialogue;
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();
        if (controller != null)
        {
            DialogueManager.instance.EnqueueDialogue(cutsceneDialogue);
            Destroy(gameObject);
        }
    }
}
