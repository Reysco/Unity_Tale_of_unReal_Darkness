using Bardent.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class WeaponInventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryWeapon> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryWeapon>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryWeapon>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryWeapon.GetEmptyItem());
            }
        }

        public int AddItem(WeaponDataSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(WeaponDataSO item, int quantity)
        {
            InventoryWeapon newItem = new InventoryWeapon
            {
                item = item,
                quantity = quantity,
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmpty).Any() == false;


        private int AddStackableItem(WeaponDataSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public void AddItem(InventoryWeapon item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryWeapon> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryWeapon> returnValue = new Dictionary<int, InventoryWeapon>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryWeapon GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryWeapon item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryWeapon
    {
        public int quantity;
        public WeaponDataSO item;
        public bool IsEmpty => item == null;

        public InventoryWeapon ChangeQuantity(int newQuantity)
        {
            return new InventoryWeapon
            {
                item = this.item,
                quantity = newQuantity
            };
        }

        public static InventoryWeapon GetEmptyItem()
            => new InventoryWeapon
            {
                item = null,
                quantity = 0,
            };
    }

}
