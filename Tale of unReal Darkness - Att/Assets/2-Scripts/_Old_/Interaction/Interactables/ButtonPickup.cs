using System;
using Bardent.CoreSystem;
using Bardent.UI;
using Bardent.Weapons;
using Inventory.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bardent.Interaction.Interactables
{
    public class ButtonPickup : MonoBehaviour, IInteractable<WeaponDataSO>
    {
        public WeaponDataSO weaponData;

        public WeaponSwap weaponSwap;

        public ItemActionPanel itemActionPanel;


        public WeaponDataSO GetContext() => weaponData;

        private void Start()
        {
            itemActionPanel = GetComponentInParent<ItemActionPanel>();
            weaponData = itemActionPanel.weaponDataSO;
        }

        public void SetContext(WeaponDataSO context)
        {
            weaponData = context;
        }

        public void Interact()
        {
            //weaponData = itemActionPanel.weaponDataSO;
            weaponSwap.HandleTryInteractButton(this);
            //Destroy(gameObject);

        }

        public void EnableInteraction()
        {
            
        }

        public void DisableInteraction()
        {
            
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Awake()
        {
            weaponSwap = FindObjectOfType<WeaponSwap>();

            if (weaponData is null)
                return;            
        }
    }
}