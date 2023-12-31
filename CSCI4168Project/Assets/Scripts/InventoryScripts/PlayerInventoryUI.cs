using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class used to update the players inventory UI
 */
public class PlayerInventoryUI : MonoBehaviour
{
    private TextMeshProUGUI itemText;
    private PlayerInventoryControl inventoryControl;
    private Inventory playerInventory;
    private GameObject[] slots;
    private int currItemSlot;
    private Color white = new Color(1f, 1f, 1f, (float)(175.0/255.0));
    private Color green = new Color(0, 1f, 0, (float)(175.0/255.0));
    
    private void Start()
    {
        // initialize variables
        inventoryControl = PlayerInventoryControl.instance;
        playerInventory = InventoryManager.Instance.GetInventory();
        currItemSlot = inventoryControl.GetHeldItemIndex();
        itemText = GameObject.Find("ItemHeldText").GetComponent<TextMeshProUGUI>();
        OnInventoryUpdate();
        if (playerInventory != null)
        {
            playerInventory.InventoryUpdate += OnInventoryUpdate;
        }
        
    }

    private void Update()
    {
        if (currItemSlot != inventoryControl.GetHeldItemIndex())
        {
            // set held item to green box background
            if (currItemSlot >= 0 && currItemSlot < playerInventory.Size())
            {
                GetSlotImage(currItemSlot).color = white;

            }
            
            currItemSlot = inventoryControl.GetHeldItemIndex();
            GetSlotImage(currItemSlot).color = green;
            if (playerInventory.GetItem(currItemSlot) != null)
            {
                itemText.text = playerInventory.GetItem(currItemSlot).itemName;
            }
            else
            {
                itemText.text = "";
            }
            
        }
        
    }
    // utility function to check if inventory got updated
    void OnInventoryUpdate()
    {
        Debug.Log("Inventory Updated");
        for (int i = 0; i < playerInventory.Size(); i++)
        {
            HoldableItem item = playerInventory.GetItem(i);
            RawImage itemImage = GetSlotItemImage(i);
            if (item != null && item.image != null)
            {
                itemImage.texture = item.image;
                itemImage.color = Color.white;
            }
            else
            {
                itemImage.texture = null;
                itemImage.color = Color.clear;
            }
        }
    }
    
    private void OnDestroy()
    {
        if (playerInventory != null)
        {
            playerInventory.InventoryUpdate -= OnInventoryUpdate;
        }
    }

    private Image GetSlotImage(int index)
    {
        return transform.GetChild(index).GameObject().GetComponent<Image>();
    }

    private RawImage GetSlotItemImage(int index)
    {
        return transform.GetChild(index).GetChild(0).GetComponent<RawImage>();
    }
    
}
