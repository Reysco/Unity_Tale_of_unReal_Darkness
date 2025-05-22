using Bardent;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUpSystem : MonoBehaviour
{
    [SerializeField]
    private WeaponInventorySO inventoryData;

    public HelpCanvas helpCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponItem item = collision.GetComponent<WeaponItem>();
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