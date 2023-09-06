using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuButton : MonoBehaviour
{
  
    public GameObject menuPopup;
    public Sprite exitButton;
    public Sprite defaultButton;
    private bool isOpenMenuButton;
    private bool isHidden;
    private Image button;

    void Start()
    {
        isOpenMenuButton = true;
        isHidden = false;
        button = gameObject.GetComponent<Image>();
    

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOpenMenuButton == true)
        {
            switch (isHidden)
            {
                case false:
                    button.enabled = false;
                    isHidden = true;
                    break;

                case true:
                    button.enabled = true;
                    isHidden = false;
                    break;
            }


        }
    }
    public void Click()
    {
        if (isOpenMenuButton == true && isHidden == false)
        {
            PlayerMovement state = FindObjectOfType<PlayerMovement>();
            if (state.currentState == PlayerState.interact)
            {
                return;
            }
            state.currentState = PlayerState.interact;

            menuPopup.SetActive(true);
            gameObject.GetComponent<Image>().overrideSprite = exitButton;
            isOpenMenuButton = false;
        }
        else if (isOpenMenuButton == false && isHidden == false)
        {
            menuPopup.SetActive(false);
            gameObject.GetComponent<Image>().overrideSprite = null;
            isOpenMenuButton = true;
            PlayerMovement state = FindObjectOfType<PlayerMovement>();
            state.currentState = PlayerState.walk;
        }



    }
}


