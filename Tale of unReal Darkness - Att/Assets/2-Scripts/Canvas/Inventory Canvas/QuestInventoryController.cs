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
    public class QuestInventoryController : MonoBehaviour
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
        public QuestInventorySO inventoryData;

        public List<InventoryQuest> initialItems = new List<InventoryQuest>();

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
            foreach (InventoryQuest item in initialItems)
            {
                if(item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryQuest> inventoryState)
        {
            questUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                questUI.UpdateData(item.Key, item.Value.item.Icon, 
                    item.Value.quantity); //teste
            }
        }

        private void PreparaUI()
        {
            questUI.InitializeInventoryUI(inventoryData.Size);
            this.questUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.questUI.OnSwapItems += HandleSwapItems;
            this.questUI.OnStartDragging += HandleDragging;
            this.questUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryQuest inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                questUI.ShowItemAction(itemIndex);
                questUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                questUI.AddData(inventoryItem.item);
            }

        }

        public void PerformAction(int itemIndex)
        {
            InventoryQuest inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;


            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    questUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryQuest inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            questUI.CreateDraggedItem(inventoryItem.item.Icon, inventoryItem.quantity); //teste
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryQuest inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                questUI.ResetSelection();
                return;
            }
            QuestSO item = inventoryItem.item;
            questUI.UpdateDescription(itemIndex, item.Icon,
                item.name, item.Description);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                if(questUI.isActiveAndEnabled == false)
                {
                    Time.timeScale = 0;
                    questUI.Show();
                    toggleUI.Show();
                    statusUI.Hide();
                    pauseGameUI.Hide();
                    weaponUI.Hide();
                    inventoryUI.Hide();

                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        questUI.UpdateData(item.Key,
                            item.Value.item.Icon,
                            item.Value.quantity); //teste
                    }
                }
                else
                {
                    Time.timeScale = 1;
                    questUI.Hide();
                    toggleUI.Hide();

                }
            }
        }
    }
}
