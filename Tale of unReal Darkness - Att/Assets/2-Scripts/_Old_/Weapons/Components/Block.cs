using System;
using Bardent.CoreSystem;
using Bardent.Utilities;
using Bardent.Weapons.Modifiers;
using UnityEngine;

namespace Bardent.Weapons.Components
{
    public class Block : WeaponComponent<BlockData, AttackBlock>
    {
        // Event fired off when an attack is blocked. The parameter is the GameObject of the entity that was blocked
        public event Action<GameObject> OnBlock;

        // The players DamageReceiver core component. We use the damage receiver's modifiers to modify the amount of damage successfully blocked attacks deal.
        private DamageReceiverPlayer damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiverPlayer poiseDamageReceiver;
        private FireDamageReceiverPlayer fireDamageReceiver;
        private BleedingDamageReceiverPlayer bleedingDamageReceiver;
        private FreezeDamageReceiverPlayer freezeDamageReceiver;
        private PoisonDamageReceiverPlayer poisonDamageReceiver;
        private SleepDamageReceiverPlayer sleepDamageReceiver;

        // The modifier that we give the DamageReceiver when the block window is active.
        private DamageModifier damageModifier;
        private BlockKnockBackModifier knockBackModifier;
        private BlockPoiseDamageModifier poiseDamageModifier;
        private FireDamageModifier fireDamageModifier;
        private BleedingDamageModifier bleedingDamageModifier;
        private FreezeDamageModifier freezeDamageModifier;
        private PoisonDamageModifier poisonDamageModifier;
        private SleepDamageModifier sleepDamageModifier;


        private CoreSystem.Movement movement;
        private ParticleManager particleManager;

        private bool isBlockWindowActive;
        private bool shouldUpdate;

        private float nextWindowTriggerTime;

        // Starts the block window by passing modifiers to receivers.
        private void StartBlockWindow()
        {
            isBlockWindowActive = true;
            shouldUpdate = false;

            damageModifier.OnModified += HandleModified;
            //Se ativar isso, o inimigo vai tomar dano multiplicado, se o dano de block é 5, ele toma 5x cada quantidade abaixo por exemplo
            /*fireDamageModifier.OnModified += HandleModified;
            bleedingDamageModifier.OnModified += HandleModified;
            freezeDamageModifier.OnModified += HandleModified;
            poisonDamageModifier.OnModified += HandleModified;
            sleepDamageModifier.OnModified += HandleModified;*/

            damageReceiver.Modifiers.AddModifier(damageModifier);
            knockBackReceiver.Modifiers.AddModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.AddModifier(poiseDamageModifier);
            fireDamageReceiver.Modifiers.AddModifier(fireDamageModifier);
            bleedingDamageReceiver.Modifiers.AddModifier(bleedingDamageModifier);
            freezeDamageReceiver.Modifiers.AddModifier(freezeDamageModifier);
            poisonDamageReceiver.Modifiers.AddModifier(poisonDamageModifier);
            sleepDamageReceiver.Modifiers.AddModifier(sleepDamageModifier);

        }

        // Stops block window by removing modifiers from windows.
        private void StopBlockWindow()
        {
            isBlockWindowActive = false;
            shouldUpdate = false;

            damageModifier.OnModified -= HandleModified;
            //Se ativar isso, o inimigo vai tomar dano multiplicado, se o dano de block é 5, ele toma 5x cada quantidade abaixo por exemplo
            /*fireDamageModifier.OnModified -= HandleModified;
            bleedingDamageModifier.OnModified -= HandleModified;
            freezeDamageModifier.OnModified -= HandleModified;
            poisonDamageModifier.OnModified -= HandleModified;
            sleepDamageModifier.OnModified -= HandleModified;*/

            damageReceiver.Modifiers.RemoveModifier(damageModifier);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
            fireDamageReceiver.Modifiers.RemoveModifier(fireDamageModifier);
            bleedingDamageReceiver.Modifiers.RemoveModifier(bleedingDamageModifier);
            freezeDamageReceiver.Modifiers.RemoveModifier(freezeDamageModifier);
            poisonDamageReceiver.Modifiers.RemoveModifier(poisonDamageModifier);
            sleepDamageReceiver.Modifiers.RemoveModifier(sleepDamageModifier);

        }

        // Checks if source falls withing any blocked regions for the current attack. Also returns the block information
        private bool IsAttackBlocked(Transform source, out DirectionalInformation directionalInformation)
        {
            var angleOfAttacker = AngleUtilities.AngleFromFacingDirection(
                Core.Root.transform,
                source,
                movement.FacingDirection
            );

            return currentAttackData.IsBlocked(angleOfAttacker, out directionalInformation);
        }

        /*
         * The modifier is what tells us if a block was performed. It fires off an event when used. This handles that event and broadcasts
         * that information further
         */
            private void HandleModified(GameObject source)
            {
                particleManager.StartWithRandomRotation(currentAttackData.Particles, currentAttackData.ParticlesOffset);

                OnBlock?.Invoke(source);
            }


        private void HandleEnterAttackPhase(AttackPhases phase)
        {
            shouldUpdate = isBlockWindowActive
                ? currentAttackData.BlockWindowEnd.TryGetTriggerTime(phase, out nextWindowTriggerTime)
                : currentAttackData.BlockWindowStart.TryGetTriggerTime(phase, out nextWindowTriggerTime);
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            particleManager = Core.GetCoreComponent<ParticleManager>();

            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            damageReceiver = Core.GetCoreComponent<DamageReceiverPlayer>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiverPlayer>();
            fireDamageReceiver= Core.GetCoreComponent<FireDamageReceiverPlayer>();
            bleedingDamageReceiver= Core.GetCoreComponent<BleedingDamageReceiverPlayer>();
            freezeDamageReceiver= Core.GetCoreComponent<FreezeDamageReceiverPlayer>();
            poisonDamageReceiver= Core.GetCoreComponent<PoisonDamageReceiverPlayer>();
            sleepDamageReceiver= Core.GetCoreComponent<SleepDamageReceiverPlayer>();


            // Create the modifier objects.
            damageModifier = new DamageModifier(IsAttackBlocked);
            knockBackModifier = new BlockKnockBackModifier(IsAttackBlocked);
            poiseDamageModifier = new BlockPoiseDamageModifier(IsAttackBlocked);
            fireDamageModifier = new FireDamageModifier(IsAttackBlocked);
            bleedingDamageModifier = new BleedingDamageModifier(IsAttackBlocked);
            freezeDamageModifier = new FreezeDamageModifier(IsAttackBlocked);
            poisonDamageModifier = new PoisonDamageModifier(IsAttackBlocked);
            sleepDamageModifier = new SleepDamageModifier(IsAttackBlocked);


            AnimationEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
        }

        private void Update()
        {
            if (!shouldUpdate || !IsPastTriggerTime())
                return;

            if (isBlockWindowActive)
            {
                StopBlockWindow();
            }
            else
            {
                StartBlockWindow();
            }
        }

        private bool IsPastTriggerTime()
        {
            return Time.time >= nextWindowTriggerTime;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
        }

        #endregion
    }
}