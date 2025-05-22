using Bardent;
using Bardent.UI;
using Bardent.Weapons;
using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Bardent.Weapons.WeaponDataSO;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private UIInventoryPage weaponUI;

        [SerializeField]
        private UIInventoryPage questUI;

        [SerializeField]
        private UIStatusPage statusUI;

        [SerializeField]
        private UIPauseGamePage pauseGameUI;

        [SerializeField]
        private UITogglePage toggleUI;

        [SerializeField]
        private HelpCanvas helpCanvas;

        [SerializeField]
        public WeaponInventorySO inventoryData;

        public List<InventoryWeapon> initialItems = new List<InventoryWeapon>();

        //[Header("----------- Weapon Status -------------")]
        //public bool firstWeaponButtonClicked;
        //public bool firstWeaponSelected;
        //public Image provFirstWeaponImage;

        //public bool secondWeaponButtonClicked;
        //public bool secondWeaponSelected;
        //public Image provSecondWeaponImage;


        private void Start()
        {
            PreparaUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryWeapon item in initialItems)
            {
                if(item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryWeapon> inventoryState)
        {
            weaponUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                weaponUI.UpdateData(item.Key, item.Value.item.Icon, 
                    item.Value.quantity, item.Value.item); //teste
            }
        }

        private void PreparaUI()
        {
            weaponUI.InitializeInventoryUI(inventoryData.Size);
            this.weaponUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.weaponUI.OnSwapItems += HandleSwapItems;
            this.weaponUI.OnStartDragging += HandleDragging;
            this.weaponUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryWeapon inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                weaponUI.ShowItemAction(itemIndex);
                weaponUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                weaponUI.AddData(inventoryItem.item);
            }

        }

        public void PerformAction(int itemIndex)
        {
            InventoryWeapon inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;


            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    weaponUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryWeapon inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            weaponUI.CreateDraggedItem(inventoryItem.item.Icon, inventoryItem.quantity ,inventoryItem.item); //teste
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryWeapon inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                weaponUI.ResetSelection();
                return;
            }
            WeaponDataSO item = inventoryItem.item;
            weaponUI.UpdateDescription(itemIndex, item.Icon,
                item.name, item.Description);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.U))
            {
                if(weaponUI.isActiveAndEnabled == false)
                {
                    Time.timeScale = 0;
                    weaponUI.Show();
                    toggleUI.Show();
                    statusUI.Hide();
                    inventoryUI.Hide();
                    questUI.Hide();
                    pauseGameUI.Hide();

                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        weaponUI.UpdateData(item.Key,
                            item.Value.item.Icon,
                            item.Value.quantity,
                            item.Value.item); //teste
                    }
                }
                else
                {
                    Time.timeScale = 1;
                    weaponUI.Hide();
                    toggleUI.Hide();

                }
            }
        }
    }
}
