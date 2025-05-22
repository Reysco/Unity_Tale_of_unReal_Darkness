using Bardent.UI;
using Bardent.Weapons;
using Inventory;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class UIButtonClicked : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage itemInventoryUI;
        [SerializeField] private UIInventoryPage weaponInventoryUI;
        [SerializeField] private UIInventoryPage questInventoryUI;
        [SerializeField] private UIStatusPage statusUI;
        [SerializeField] private UIPauseGamePage pauseGameUI;
        [SerializeField] private UITogglePage toggleUI;


        public void Menu()
        {
            pauseGameUI.Show();
            statusUI.Hide();
            itemInventoryUI.Hide();
            weaponInventoryUI.Hide();
            questInventoryUI.Hide();
            toggleUI.Hide();
        }

        public void StatusInventory()
        {
            pauseGameUI.Hide();
            statusUI.Show();
            itemInventoryUI.Hide();
            weaponInventoryUI.Hide();
            questInventoryUI.Hide();
        }

        public void WeaponInventory()
        {
            pauseGameUI.Hide();
            statusUI.Hide();
            itemInventoryUI.Show();
            weaponInventoryUI.Hide();
            questInventoryUI.Hide();
        }

        public void ItemsInventory()
        {
            pauseGameUI.Hide();
            statusUI.Hide();
            itemInventoryUI.Hide();
            weaponInventoryUI.Show();
            questInventoryUI.Hide();
        }

        public void QuestInventory()
        {
            pauseGameUI.Hide();
            statusUI.Hide();
            itemInventoryUI.Hide();
            weaponInventoryUI.Hide();
            questInventoryUI.Show();
        }

        public void ExitInventory()
        {
            Time.timeScale = 1;

            pauseGameUI.Hide();
            statusUI.Hide();
            itemInventoryUI.Hide();
            weaponInventoryUI.Hide();
            questInventoryUI.Hide();
            toggleUI.Hide();

            
        }




    }
}
