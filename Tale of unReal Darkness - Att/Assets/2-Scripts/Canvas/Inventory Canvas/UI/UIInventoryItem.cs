using Bardent.CoreSystem;
using Bardent.ProjectileSystem;
using Bardent.Weapons;
using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, 
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        private GameObject image;

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, 
            OnRightMouseBtnClick;

        private bool empty = true;

        public WeaponDataSO weaponData;

        [Header("======= Verificar se a arma é igual ========")]
        public WeaponInventory weaponInventory; //teste
        public bool sameWeapon;
        public GameObject equippedPanel;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        private void Start()
        {
            weaponInventory= FindObjectOfType<WeaponInventory>();
        }
        

        private void Update()
        {

            //RESPONSAVEL PELA VERIFICAÇAO SE O ITEM É IGUAL, CASO SEJA APARECE QUE ESTÁ EQUIPADO
            if ((weaponData == weaponInventory.weaponData[0] || (weaponData == weaponInventory.weaponData[1])) && weaponData != null)
            {
                sameWeapon = true;                
                equippedPanel.SetActive(true);
            }
            else
            {
                sameWeapon = false;
                equippedPanel.SetActive(false);
            }

            if(image.activeSelf == false)
            {
                sameWeapon = false;
                equippedPanel.SetActive(false);
            }

        }

        public void ResetData()
        {
            if(gameObject!= null)
            {
                this.itemImage.gameObject.SetActive(false);
                empty = true;
            }
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity, WeaponDataSO weaponType)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTxt.text = quantity + "";
            this.weaponData = weaponType;
            empty = false;                     
        }
        
        public void Select()
        {
            borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                if(equippedPanel.activeSelf == false)
                {
                    OnRightMouseBtnClick?.Invoke(this);
                }
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }










        public void SetData(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTxt.text = quantity + "";
            empty = false;
        }
    }
    
}
