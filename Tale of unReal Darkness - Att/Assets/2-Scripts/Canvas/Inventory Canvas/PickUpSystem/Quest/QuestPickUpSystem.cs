using Bardent;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPickUpSystem : MonoBehaviour
{
    [SerializeField]
    private QuestInventorySO inventoryData;

    public HelpCanvas helpCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Quest item = collision.GetComponent<Quest>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryQuest, item.Quantity);
            if (reminder == 0)
                item.DestroyQuest();
            else
                item.Quantity = reminder;


            if(helpCanvas.FirstTimeInventory != null)
            {
                helpCanvas.FirstTimeInventory.SetActive(true);
            }            
        }        
    }
}