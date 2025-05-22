using System;
using System.Collections;
using Bardent.Combat.Damage;
using Bardent.CoreSystem.StatsSystem;
using Bardent.ModifierSystem;
using Unity.Services.Analytics.Platform;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class StatsPlayer : CoreComponent
    {
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat Poise { get; private set; }
        [field: SerializeField] public Stat Sleep { get; private set; }
        [field: SerializeField] public Stat Fire { get; private set; }
        [field: SerializeField] public Stat Poison { get; private set; }
        [field: SerializeField] public Stat Freeze { get; private set; }
        [field: SerializeField] public Stat Bleeding { get; private set; }

        [SerializeField] public float poiseRecoveryRate;
        [SerializeField] public float sleepRecoveryRate;
        [SerializeField] public float fireRecoveryRate;
        [SerializeField] public float poisonRecoveryRate;
        [SerializeField] public float freezeRecoveryRate;
        [SerializeField] public float bleedingRecoveryRate;

        [SerializeField] //Verificar e ativar fogo
        public GameObject fire;
        public bool onFired;

        [SerializeField] //Verificar e ativar veneno
        public GameObject poison;
        public bool onPoisoned;

        [SerializeField] //Verificar e ativar congelamento
        public GameObject freeze;
        public bool onFreezed;

        [SerializeField] //Particula do bleeding
        public ParticleSystem bleeding;

        public DamageReceiverPlayer damageReceiver; //verificar quantidade de dano que toma por hit se precisar

        [Header("===================Player Data Acess===================")]
        [SerializeField] //Acesso aos movimentos
        private PlayerData playerData;
        public float moveState; //lembrar de adicionar o valor manualmente (é o mesmo do D_MoveState)
        public float jumpState;
        public float wallJumpState;
        public float wallSlideState;
        public float crouchMoveState;
        public float dashState;


        protected override void Awake()
        {
            base.Awake();

            Health.Init();
            Poise.Init();
            Sleep.Init();
            Fire.Init();
            Poison.Init();
            Freeze.Init();
            Bleeding.Init();


        }

        private void Start()
        {
            playerData.movementVelocity = moveState;
            playerData.jumpVelocity = jumpState;
            playerData.wallJumpVelocity = wallJumpState;
            playerData.wallSlideVelocity = wallSlideState;
            playerData.crouchMovementVelocity = crouchMoveState;
            playerData.dashVelocity = dashState;

        }

        private void Update()
        {
            //faz perder vida, porem nessa funçao update ele perde vida toda hora até zerar
            //Health.Decrease(0.01f);

            PoisonState();
            FireState();
            PoiseState();
            SleepState();
            FreezeState();
            BleedingState();
        }


        private void PoisonState()
        {
            if (Poison.CurrentValue.Equals(Poison.MaxValue))
                return;

            Poison.Increase(poisonRecoveryRate * Time.deltaTime);

            if (Poison.CurrentValue <= poisonRecoveryRate)
            {
                onPoisoned = true;
                poison.gameObject.SetActive(true);
                playerData.movementVelocity = 1.5f;

            }
            else if (Poison.CurrentValue >= Poison.MaxValue)
            {
                onPoisoned = false;
                poison.gameObject.SetActive(false);
                playerData.movementVelocity = moveState;
            }

            if (onPoisoned == true)
            {
                StartCoroutine("poisonCounter");
            }
        }

        private void FireState()
        {
            if (Fire.CurrentValue.Equals(Fire.MaxValue))
                return;

            Fire.Increase(fireRecoveryRate * Time.deltaTime);

            if (Fire.CurrentValue <= fireRecoveryRate)
            {
                onFired = true;
                fire.gameObject.SetActive(true);
            }
            else if (Fire.CurrentValue >= Fire.MaxValue)
            {
                onFired = false;
                fire.gameObject.SetActive(false);
            }

            if (onFired == true)
            {
                StartCoroutine("fireCounter");
            }
        }

        private void PoiseState()
        {
            if (Poise.CurrentValue.Equals(Poise.MaxValue))
                return;

            Poise.Increase(poiseRecoveryRate * Time.deltaTime);

            if ((Poise.CurrentValue <= poiseRecoveryRate)) //Sistema para que o player nao mantenha o inimigo infinitamente dormindo ao dar dano continuo
            {
                Poise.currentValue = Poise.MaxValue;
            }
        }

        private void SleepState()
        {
            if (Sleep.CurrentValue.Equals(Sleep.MaxValue))
                return;

            Sleep.Increase(sleepRecoveryRate * Time.deltaTime);

            if ((Sleep.CurrentValue <= sleepRecoveryRate)) //Sistema para que o player nao mantenha o inimigo infinitamente stunado ao dar dano continuo
            {
                Sleep.currentValue = Sleep.MaxValue;
            }
        }

        private void FreezeState()
        {
            if (Freeze.CurrentValue.Equals(Freeze.MaxValue))
                return;

            Freeze.Increase(freezeRecoveryRate * Time.deltaTime);

            if (Freeze.CurrentValue <= freezeRecoveryRate)
            {
                onFreezed = true;
                freeze.gameObject.SetActive(true);
                playerData.movementVelocity = 0;
                playerData.jumpVelocity = 0;
                playerData.wallJumpVelocity = 0;
                playerData.wallSlideVelocity = 15;
                playerData.crouchMovementVelocity = 0;
                playerData.dashVelocity = 0;

            }
            else if (Freeze.CurrentValue >= Freeze.MaxValue)
            {
                onFreezed = false;
                freeze.gameObject.SetActive(false);
                playerData.movementVelocity = moveState;
                playerData.jumpVelocity = jumpState;
                playerData.wallJumpVelocity = wallJumpState;
                playerData.wallSlideVelocity = wallSlideState;
                playerData.crouchMovementVelocity = crouchMoveState;
                playerData.dashVelocity = dashState;
            }
        }

        private void BleedingState()
        {
            if (Bleeding.CurrentValue.Equals(Bleeding.MaxValue))
                return;

            Bleeding.Increase(bleedingRecoveryRate * Time.deltaTime);

            if ((Bleeding.CurrentValue <= bleedingRecoveryRate)) //Sistema para que o player nao mantenha o inimigo infinitamente stunado ao dar dano continuo
            {
                Health.Decrease((int)(Health.MaxValue * 0.15f)); //sangramento em 15% da vida maxima
                Bleeding.currentValue = Bleeding.MaxValue;
                Instantiate(this.bleeding, this.transform.position, Quaternion.identity);
            }

        }

        IEnumerator fireCounter()
        {
            yield return new WaitForSeconds(1f); // a cada 1 segundo perde vida
            Health.Decrease(5f); //quantidade de vida removida por segundo
            StopCoroutine("fireCounter");
        }

        IEnumerator poisonCounter()
        {
            yield return new WaitForSeconds(1f); // a cada 1 segundo perde vida
            Health.Decrease(1f); //quantidade de vida removida por segundo
            StopCoroutine("poisonCounter");
        }

        //Funçao para perder ou ganhar vida ao colidir com gameobject com determinada TAG (tem que ter boxcollider2d no stats)
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "HitKill")
            {
                Health.Decrease(100000f);
            }

            else if (collision.gameObject.layer == 6)
            {
                Health.Increase(5f);
            }
        }
    }
}

