using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{


    public Image icon;
    public Text itemNameText;
    //public Text itemDescriptionText;
    public Item item;

    private void Start()
    {
        itemNameText.enabled = false;
        //itemDescriptionText.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        icon.overrideSprite = newItem.itemIcon;
        item = newItem;
        itemNameText.text = newItem.itemName;
       //itemDescriptionText.text = newItem.itemDescription;
        itemNameText.enabled = true;
        //itemDescriptionText.enabled = true;
    }

    public void RemoveItem()
    {
        icon.overrideSprite = null;
        item = null;
        itemNameText.enabled = false;
        //itemDescriptionText.enabled = false;
    }
}


