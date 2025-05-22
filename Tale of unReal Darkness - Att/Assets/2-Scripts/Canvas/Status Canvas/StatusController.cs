using Inventory;
using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


public class StatusController : MonoBehaviour
{
    [SerializeField]
    private UIStatusPage statusUI;

    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private UIInventoryPage weaponUI;

    [SerializeField]
    private UIInventoryPage questUI;

    [SerializeField]
    private UIPauseGamePage pauseGameUI;

    [SerializeField]
    private UITogglePage toggleUI;

    public InventoryController inventoryController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (statusUI.isActiveAndEnabled == false)
            {
                Time.timeScale = 0;
                toggleUI.Show();
                statusUI.Show();
                inventoryUI.Hide();
                pauseGameUI.Hide();
                weaponUI.Hide();
                questUI.Hide();


            }
            else
            {
                Time.timeScale = 1;
                statusUI.Hide();
                toggleUI.Hide();

            }
        }
    }
}

