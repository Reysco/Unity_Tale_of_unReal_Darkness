using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class HelpCanvas : MonoBehaviour
    {
        public WeaponPickUpSystem pickUp;
        public UIInventoryPage inventoryPage;

        [Header("======= Help Components =======")]
        public GameObject FirstTimeInventory;

        private void Awake()
        {
            FirstTimeInventory.SetActive(false);
        }

        private void Update()
        {
            if (inventoryPage.gameObject.activeSelf == true)
            {
                Destroy(FirstTimeInventory);
            }
        }
    }
}
