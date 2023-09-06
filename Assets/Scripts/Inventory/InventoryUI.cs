using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.root.gameObject);
    }
    public GameObject menuPopup;
    [HideInInspector] public InventorySlot[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        InventoryManager.instance.onItemAddCallBack += UpdateInventoryAdd;
        InventoryManager.instance.onItemRemoveCallBack += UpdateInventoryRemove;

        menuPopup.SetActive(false);
    }
    private int? GetNextEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) return i;
        }
        return null;
    }

    private int? GetSameSlot(Item newItem)
    {
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item == newItem) return i;
            }
        }
        return null;
    }

    public void UpdateInventoryRemove(Item newItem)
    {
        slots[(int)GetSameSlot(newItem)].RemoveItem();
    }

    public void UpdateInventoryAdd(Item newItem)
    {
        slots[(int)GetNextEmptySlot()].AddItem(newItem);
    }
}
