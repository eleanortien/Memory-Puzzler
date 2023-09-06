using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    float originalSize;
    public Image healthBarPortrait;
    public Image healthBar;
    public Image healthBarBackground;
    public Image healthPortraitBackground;
    private bool isHidden;

    private void Awake()
    {
        instance = this;  
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    public void SetValue (float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (isHidden)
            {
                case false:
                    healthBar.enabled = false;
                    healthBarBackground.enabled = false;
                    healthPortraitBackground.enabled = false;
                    healthBarPortrait.enabled = false;
                    isHidden = true;
                    break;

                case true:
                    healthBar.enabled = true;
                    healthBarBackground.enabled = true;
                    healthPortraitBackground.enabled = true;
                    healthBarPortrait.enabled = true;
                    isHidden = false;
                    break;
            }


        }
    }
}

