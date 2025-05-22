using Bardent;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpSystem : MonoBehaviour
{
    [SerializeField]
    private ItemInventorySO inventoryData;

    public HelpCanvas helpCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;


            if(helpCanvas.FirstTimeInventory != null)
            {
                helpCanvas.FirstTimeInventory.SetActive(true);
            }            
        }        
    }
}