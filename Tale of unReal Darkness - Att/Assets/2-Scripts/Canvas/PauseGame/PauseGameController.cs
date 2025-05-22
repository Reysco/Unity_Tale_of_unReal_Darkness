using Inventory;
using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


public class PauseGameController : MonoBehaviour
{
    [SerializeField]
    private UIPauseGamePage pauseGameUI;

    [SerializeField]
    private UIStatusPage statusUI;

    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private UIInventoryPage weaponUI;

    [SerializeField]
    private UIInventoryPage questUI;

    [SerializeField]
    private UITogglePage toggleUI;

    public InventoryController inventoryController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGameUI.isActiveAndEnabled == false)
            {
                Time.timeScale = 0;
                pauseGameUI.Show();
                statusUI.Hide();
                inventoryUI.Hide();
                toggleUI.Hide();
                weaponUI.Hide();
                questUI.Hide();
            }
            else
            {
                Time.timeScale = 1;
                pauseGameUI.Hide();
            }
        }
    }
}

