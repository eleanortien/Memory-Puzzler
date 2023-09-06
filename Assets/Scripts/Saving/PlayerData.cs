using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public List<string> puzzleSolved = new List<string>();
    public List<Item> itemsList = new List<Item>();


    public PlayerData(PlayerMovement player)
    {
        PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);
        if (itemsList != InventoryManager.instance.inventory)
        {
            foreach (Item item in itemsList)
            {
                if (InventoryManager.instance.inventory.Contains(item))
                {

                }
                else
                {
                    InventoryManager.instance.AddItem(item);
                }
            }

            if (itemsList.Count < InventoryManager.instance.inventory.Count)
            {
                foreach (Item item in InventoryManager.instance.inventory)
                {
                    if (itemsList.Contains(item))
                    {

                    }
                    else
                    {
                        InventoryManager.instance.RemoveItem(item);
                    }
                }
            }
        }
    }
}
