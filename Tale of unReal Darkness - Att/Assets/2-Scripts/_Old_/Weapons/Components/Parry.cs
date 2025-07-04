﻿using System;
using Bardent.CoreSystem;
using Bardent.Utilities;
using Bardent.Weapons.Modifiers;
using UnityEngine;
using static Bardent.Combat.Parry.CombatParryUtilities;

namespace Bardent.Weapons.Components
{
    /*
     * Parry works essentially the same as the Block weapon component. It passes modifiers to the various
     * player -Receiver Core Components while the parry window is active. If the damage modifier is triggered
     * it counts as a successful parry and the entity that tried to do damage is parried.
     */
    public class Parry : WeaponComponent<ParryData, AttackParry>
    {
        public event Action<GameObject> OnParry;

        private DamageReceiverPlayer damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiverPlayer poiseDamageReceiver;
        private FireDamageReceiverPlayer fireDamageReceiver;
        private BleedingDamageReceiverPlayer bleedingDamageReceiver;
        private FreezeDamageReceiverPlayer freezeDamageReceiver;
        private PoisonDamageReceiverPlayer poisonDamageReceiver;
        private SleepDamageReceiverPlayer sleepDamageReceiver;

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

        private void StartParryWindow()
        {
            isBlockWindowActive = true;
            shouldUpdate = false;

            damageModifier.OnModified += HandleParry;
            //Se ativar isso, o inimigo vai tomar dano multiplicado, se o dano de block é 5, ele toma 5x cada quantidade abaixo por exemplo
            /*fireDamageModifier.OnModified += HandleParry;
            bleedingDamageModifier.OnModified += HandleParry;
            freezeDamageModifier.OnModified += HandleParry;
            poisonDamageModifier.OnModified += HandleParry;
            sleepDamageModifier.OnModified += HandleParry;*/


            damageReceiver.Modifiers.AddModifier(damageModifier);
            knockBackReceiver.Modifiers.AddModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.AddModifier(poiseDamageModifier);
            fireDamageReceiver.Modifiers.AddModifier(fireDamageModifier);
            bleedingDamageReceiver.Modifiers.AddModifier(bleedingDamageModifier);
            freezeDamageReceiver.Modifiers.AddModifier(freezeDamageModifier);
            poisonDamageReceiver.Modifiers.AddModifier(poisonDamageModifier);
            sleepDamageReceiver.Modifiers.AddModifier(sleepDamageModifier);
        }

        private void StopParryWindow()
        {
            isBlockWindowActive = false;
            shouldUpdate = false;

            damageModifier.OnModified -= HandleParry;
            /*fireDamageModifier.OnModified -= HandleParry;
            bleedingDamageModifier.OnModified -= HandleParry;
            freezeDamageModifier.OnModified -= HandleParry;
            poisonDamageModifier.OnModified -= HandleParry;
            sleepDamageModifier.OnModified -= HandleParry;*/

            damageReceiver.Modifiers.RemoveModifier(damageModifier);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
            fireDamageReceiver.Modifiers.RemoveModifier(fireDamageModifier);
            bleedingDamageReceiver.Modifiers.RemoveModifier(bleedingDamageModifier);
            freezeDamageReceiver.Modifiers.RemoveModifier(freezeDamageModifier);
            poisonDamageReceiver.Modifiers.RemoveModifier(poisonDamageModifier);
            sleepDamageReceiver.Modifiers.RemoveModifier(sleepDamageModifier);
        }

        protected override void HandleExit()
        {
            base.HandleExit();

            damageReceiver.Modifiers.RemoveModifier(damageModifier);
            knockBackReceiver.Modifiers.RemoveModifier(knockBackModifier);
            poiseDamageReceiver.Modifiers.RemoveModifier(poiseDamageModifier);
            bleedingDamageReceiver.Modifiers.RemoveModifier(bleedingDamageModifier);
            fireDamageReceiver.Modifiers.RemoveModifier(fireDamageModifier);
            freezeDamageReceiver.Modifiers.RemoveModifier(freezeDamageModifier);
            poisonDamageReceiver.Modifiers.RemoveModifier(poisonDamageModifier);
            sleepDamageReceiver.Modifiers.RemoveModifier(sleepDamageModifier);
        }

        private bool IsAttackParried(Transform source, out DirectionalInformation directionalInformation)
        {
            var angleOfAttacker = AngleUtilities.AngleFromFacingDirection(
                Core.Root.transform,
                source,
                movement.FacingDirection
            );

            return currentAttackData.IsBlocked(angleOfAttacker, out directionalInformation);
        }

        private void HandleParry(GameObject parriedGameObject)
        {
            /*
             * The modifier is only used to detect an enemy making contact with the player from allowed directions.
             * If that happens we still need to inform the entity that it has been parried.
             */
            if (!TryParry(parriedGameObject, new Combat.Parry.ParryData(Core.Root), out _, out _))
            {
                return;
            }

            weapon.Anim.SetTrigger("parry");

            OnParry?.Invoke(parriedGameObject);

            particleManager.StartWithRandomRotation(currentAttackData.Particles, currentAttackData.ParticlesOffset);
        }

        private void HandleEnterAttackPhase(AttackPhases phase)
        {
            shouldUpdate = isBlockWindowActive
                ? currentAttackData.ParryWindowEnd.TryGetTriggerTime(phase, out nextWindowTriggerTime)
                : currentAttackData.ParryWindowStart.TryGetTriggerTime(phase, out nextWindowTriggerTime);
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            damageReceiver = Core.GetCoreComponent<DamageReceiverPlayer>();
            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiverPlayer>();
            fireDamageReceiver = Core.GetCoreComponent<FireDamageReceiverPlayer>();
            bleedingDamageReceiver = Core.GetCoreComponent<BleedingDamageReceiverPlayer>();
            freezeDamageReceiver = Core.GetCoreComponent<FreezeDamageReceiverPlayer>();
            poisonDamageReceiver = Core.GetCoreComponent<PoisonDamageReceiverPlayer>();
            sleepDamageReceiver = Core.GetCoreComponent<SleepDamageReceiverPlayer>();

            movement = Core.GetCoreComponent<CoreSystem.Movement>();
            particleManager = Core.GetCoreComponent<ParticleManager>();

            damageModifier = new DamageModifier(IsAttackParried);
            knockBackModifier = new BlockKnockBackModifier(IsAttackParried);
            poiseDamageModifier = new BlockPoiseDamageModifier(IsAttackParried);
            fireDamageModifier = new FireDamageModifier(IsAttackParried);
            bleedingDamageModifier = new BleedingDamageModifier(IsAttackParried);
            freezeDamageModifier = new FreezeDamageModifier(IsAttackParried);
            poisonDamageModifier = new PoisonDamageModifier(IsAttackParried);
            sleepDamageModifier = new SleepDamageModifier(IsAttackParried);


            AnimationEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
        }

        private void Update()
        {
            if (!shouldUpdate || !IsPastTriggerTime())
                return;

            if (isBlockWindowActive)
            {
                StopParryWindow();
            }
            else
            {
                StartParryWindow();
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