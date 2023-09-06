using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public enum PlayerState
{
    walk,
    interact
}
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    //Dialogue
    public GameObject dialogueBox;
    //State
    public PlayerState currentState;



    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("/Managers/DialogueManager/DialogueCanvas/DialogueBox");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            rb.velocity = Vector2.zero;
            {
                AdvanceDialogue();

            }
            return;
        }

        
    
        //Input Movement Commands
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        { lookDirection.Set(movement.x, movement.y);
            lookDirection.Normalize();
        }

        //Position
        //Horizontal
        if (movement.x != 0)
        {
            animator.SetFloat("PositionX", lookDirection.x);
            animator.SetFloat("PositionY", 0);
        }

        //Vertical
        if (movement.y != 0)
        {
            animator.SetFloat("PositionX", 0);
            animator.SetFloat("PositionY", lookDirection.y);
        }


        //Damage Invincible Frames
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }


        //Dialogue Raycasting (Interactions)
        if (dialogueBox.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {



                RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.2f, LayerMask.GetMask("Interactibles"));
                if (hit.collider != null)
                {
                    Interactible interactible = hit.collider.GetComponent<Interactible>();
                    CheckItemInteractible specialInteractible = hit.collider.GetComponent<CheckItemInteractible>();
                    KeypadInteractible keypadInteractible = hit.collider.GetComponent<KeypadInteractible>();
                    DoorInteractible doorInteractible = hit.collider.GetComponent<DoorInteractible>();

                    if (interactible != null)
                    {
                        interactible.TriggerDialogue();
                    }
                    else if (specialInteractible != null)
                    {
                        specialInteractible.TriggerDialogue();
                    }
                    else if (keypadInteractible != null)
                    {
                        keypadInteractible.TriggerDialogue();
                    }
                    else if (doorInteractible != null)
                    {
                        doorInteractible.TriggerDialogue();
                    }
                    else
                    {
                        NewSceneRoom newSceneRoom = hit.collider.GetComponent<NewSceneRoom>();
                        if (newSceneRoom != null)
                        {
                            newSceneRoom.TriggerDialogue();
                        }
                    }
                }

            }
        }
        
        


    }
       
    
    void FixedUpdate()
    {
        movement.Normalize();
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    
    }

    private void AdvanceDialogue()
    {

        if (Input.GetKeyDown(KeyCode.Return) && DialogueManager.instance.inDialogue != false)
        {
            DialogueManager.instance.AdvanceDialogue();

        }
    }

    public void ChangeHealth (int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        //Lose Sequence
        if (currentHealth <= 0)
        {
            GameManager.instance.NewScene("GameOver");
        }

    }
}
