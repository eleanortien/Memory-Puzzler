using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public delegate void OnItemAddCallBack(Item item);
    public OnItemAddCallBack onItemAddCallBack;
    public delegate void OnItemRemoveCallBack(Item item);
    public OnItemRemoveCallBack onItemRemoveCallBack;

    public static InventoryManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Fix this");
        }
        DontDestroyOnLoad(transform.root.gameObject);
    }
    public void AddItem(Item newItem)
    {
        inventory.Add(newItem);
        if (onItemAddCallBack != null) onItemAddCallBack.Invoke(newItem);
    }

    public void RemoveItem(Item newItem)
    {
        inventory.Remove(newItem);
        if (onItemRemoveCallBack != null) onItemRemoveCallBack.Invoke(newItem);
    }
}

