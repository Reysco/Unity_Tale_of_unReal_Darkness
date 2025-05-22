using Bardent.CoreSystem;
using Bardent.Interaction.Interactables;
using Bardent.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPref;

        public WeaponDataSO weaponDataSO;

        public ItemSO itemSO;

        public QuestSO questSO;

        public WeaponInventory weaponInventory;

        public bool teste;

        private void Update()
        {

            if ((weaponDataSO == weaponInventory.weaponData[0] || (weaponDataSO == weaponInventory.weaponData[1])) && weaponDataSO != null)
            {
                teste = true;
                Toggle(false);
            }
            else
            {
                teste = false;
            }

        }

        public void AddButton(string name, Action onClickAction)
        {

            GameObject button = Instantiate(buttonPref, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;

        }

        public void AddButtonData(WeaponDataSO weaponData)
        {
            weaponDataSO = weaponData;
        }


        internal void Toggle(bool val)
        {
            if (val == true)
                RemoveOldButtons();
            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }







        public void AddButtonData(ItemSO weaponData)
        {
            itemSO = weaponData;
        }

        internal void AddButtonData(QuestSO questData)
        {
            questSO = questData;
        }
    }
}
