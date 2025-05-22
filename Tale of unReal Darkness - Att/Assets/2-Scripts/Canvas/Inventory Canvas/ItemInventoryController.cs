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
    public class ItemInventoryController : MonoBehaviour
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
        public ItemInventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

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
            foreach (InventoryItem item in initialItems)
            {
                if(item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.Icon, 
                    item.Value.quantity); //teste
            }
        }

        private void PreparaUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                inventoryUI.AddData(inventoryItem.item);
            }

        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;


            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.Icon, inventoryItem.quantity); //teste
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.Icon,
                item.name, item.Description);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                if(inventoryUI.isActiveAndEnabled == false)
                {
                    Time.timeScale = 0;
                    inventoryUI.Show();
                    toggleUI.Show();
                    statusUI.Hide();
                    pauseGameUI.Hide();
                    weaponUI.Hide();
                    questUI.Hide();

                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key,
                            item.Value.item.Icon,
                            item.Value.quantity); //teste
                    }
                }
                else
                {
                    Time.timeScale = 1;
                    inventoryUI.Hide();
                    toggleUI.Hide();

                }
            }
        }
    }
}
