using System;
using Bardent.Interaction;
using Bardent.Interaction.Interactables;
using Bardent.UI;
using Bardent.Weapons;

namespace Bardent.CoreSystem
{
    public class WeaponSwap : CoreComponent
    {
        public event Action<WeaponSwapChoiceRequest> OnChoiceRequested;
        public event Action<WeaponDataSO> OnWeaponDiscarded;

        private InteractableDetector interactableDetector;
        private WeaponInventory weaponInventory;

        private WeaponDataSO newWeaponData;

        private WeaponPickup weaponPickup;

        private ButtonPickup buttonPickup;

        public WeaponSwapUI weaponSwapUI;



        //Responsavel pela parte de clicar no botão para receber arma
        public void HandleTryInteractButton(IInteractable interactable)
        {
            if (interactable is not ButtonPickup pickup)
                return;

            buttonPickup = pickup;

            newWeaponData = buttonPickup.GetContext();

            if (weaponInventory.TryGetEmptyIndex(out var index))
            {
                weaponInventory.TrySetWeapon(newWeaponData, index, out _);
                //interactable.Interact(); //isso aqui adiciona varios weapons ao mesmo tempo
                newWeaponData = null;
                return;
            }

            //aqui é responsavel pela mudança de arma
            weaponSwapUI.Show();
            OnChoiceRequested?.Invoke(new WeaponSwapChoiceRequest(
                HandleWeaponSwapChoice,
                weaponInventory.GetWeaponSwapChoices(),
                newWeaponData
            ));
            
        }


        private void HandleTryInteract(IInteractable interactable)
        {
            if (interactable is not WeaponPickup pickup)
                return;

            weaponPickup = pickup;

            newWeaponData = weaponPickup.GetContext();

            if (weaponInventory.TryGetEmptyIndex(out var index))
            {
                weaponInventory.TrySetWeapon(newWeaponData, index, out _);
                interactable.Interact();
                newWeaponData = null;
                return;
            }

            OnChoiceRequested?.Invoke(new WeaponSwapChoiceRequest(
                HandleWeaponSwapChoice,
                weaponInventory.GetWeaponSwapChoices(),
                newWeaponData
            ));
        }

        private void HandleWeaponSwapChoice(WeaponSwapChoice choice)
        {
            if (!weaponInventory.TrySetWeapon(newWeaponData, choice.Index, out var oldData)) 
                return;
            
            newWeaponData = null;

            OnWeaponDiscarded?.Invoke(oldData);
                
            if (weaponPickup is null)
                return;

            weaponPickup.Interact();
            
        }

        protected override void Awake()
        {
            base.Awake();

            interactableDetector = core.GetCoreComponent<InteractableDetector>();
            weaponInventory = core.GetCoreComponent<WeaponInventory>();
        }

        private void OnEnable()
        {
            interactableDetector.OnTryInteract += HandleTryInteract;
            interactableDetector.OnTryInteract += HandleTryInteractButton; //teste
        }


        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryInteract;
            interactableDetector.OnTryInteract -= HandleTryInteractButton; //teste 
        }
    }
}