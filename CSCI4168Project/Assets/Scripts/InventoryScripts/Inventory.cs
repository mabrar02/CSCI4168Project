using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public string InventoryName;
    public ItemInstance[] items = new ItemInstance[10];
    public event Action InventoryUpdate;
    
    public bool AddItem(ItemInstance itemToAdd)
    {
        // Finds an empty slot if there is one
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item == null)
            {
                items[i] = itemToAdd;
                InventoryUpdate?.Invoke();
                Debug.Log(itemToAdd.item.itemName + " has been added to Inventory " + InventoryName);
                return true;
            }
        }

        Debug.Log("No space in Inventory " + InventoryName);
        return false;
    }

    public bool RemoveItem(int index)
    {
        if (index < items.Length)
        {
            items[index].item = null;
            InventoryUpdate?.Invoke();
            return true;
        }
        
        return false;
    }

    public HoldableItem GetItem(int index)
    {
        return items[index].item;
    }

    public int Size()
    {
        return items.Length;
    }
    
    
}
