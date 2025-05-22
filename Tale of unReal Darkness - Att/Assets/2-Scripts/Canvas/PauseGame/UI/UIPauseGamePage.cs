using Inventory;
using Inventory.Model;
using Inventory.UI;
using Iventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseGamePage : MonoBehaviour
{

    [SerializeField]
    private UIStatusPage statusUI;

    [SerializeField]
    private UIInventoryPage weaponUI;

    [SerializeField]
    private UIInventoryPage questUI;

    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private UITogglePage toggleUI;

    public InventoryController inventoryController;

    [SerializeField]
    public WeaponInventorySO inventoryData;

    [Header("============= Assets Menu ===============")]
    public GameObject continueObject;
    public GameObject itemInventoryObject;
    public GameObject weaponInventoryObject;
    public GameObject questInventoryObject;
    public GameObject playerStatusObject;
    public GameObject optionsObject;
    public GameObject quitToMenuObject;
    public GameObject cautionNotice;


    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        Hide();
        Time.timeScale = 1;
    }

    public void ItemInventory()
    {
        Time.timeScale = 0;
        Hide();
        inventoryUI.Show();
        toggleUI.Show();
        statusUI.Hide();
        weaponUI.Hide();
        questUI.Hide();

        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity, 
                item.Value.item); //teste
        }
    }

    public void WeaponInventory()
    {
        Time.timeScale = 0;
        Hide();
        weaponUI.Show();
        toggleUI.Show();
        statusUI.Hide();
        inventoryUI.Hide();
        questUI.Hide();

        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            weaponUI.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity,
                item.Value.item); //teste
        }
    }

    public void QuestInventory()
    {
        Time.timeScale = 0;
        Hide();
        questUI.Show();
        toggleUI.Show();
        statusUI.Hide();
        inventoryUI.Hide();
        weaponUI.Hide();

        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            questUI.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity,
                item.Value.item); //teste
        }
    }

    public void PlayerStatus()
    {
        Time.timeScale = 0;
        Hide();
        statusUI.Show();
        toggleUI.Show();
        inventoryUI.Hide();
        weaponUI.Hide();
        questUI.Hide();
    }

    public void Options()
    {
        //falta fazer opçoes
    }

    public void QuitToMenu()
    {
        continueObject.gameObject.SetActive(false);
        itemInventoryObject.gameObject.SetActive(false);
        weaponInventoryObject.gameObject.SetActive(false);
        questInventoryObject.gameObject.SetActive(false);
        playerStatusObject.gameObject.SetActive(false);
        optionsObject.gameObject.SetActive(false);
        quitToMenuObject.gameObject.SetActive(false);
        cautionNotice.gameObject.SetActive(true);
    }

    public void YesQuitToMenu()
    {
        //ir para o menu do game
    }

    public void NoQuitToMenu()
    {
        continueObject.gameObject.SetActive(true);
        itemInventoryObject.gameObject.SetActive(true);
        weaponInventoryObject.gameObject.SetActive(true);
        questInventoryObject.gameObject.SetActive(true);
        playerStatusObject.gameObject.SetActive(true);
        optionsObject.gameObject.SetActive(true);
        quitToMenuObject.gameObject.SetActive(true);
        cautionNotice.gameObject.SetActive(false);
    }
}
