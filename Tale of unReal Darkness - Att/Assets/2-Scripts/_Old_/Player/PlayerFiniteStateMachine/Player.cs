using System;
using System.Collections;
using System.Collections.Generic;
using Bardent;
using Bardent.CoreSystem;
using Bardent.FSM;
using Bardent.Weapons;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    public PlayerStunState PlayerStunState { get; private set; }
    public PlayerSlepState PlayerSleepState { get; private set; }

    #endregion

    #region New Variables
    [SerializeField]
    private PlayerData playerData;

    private SimpleWind simpleWind;    

    [Header("------Active or Desactive Habilities--------")]
    public bool dashStatus;
    public bool grabStatus;
    public bool wallJumpStatus;

    [Header("------Set Variables--------")]
    public float playerDataWallJumpVelocity; //esse é para salvar o valor atual da velocidade do player de walljump = 20
    public float playerDataWallJumpTime; //esse é para salvar o valor atual do tempo de walljump do player = 0.4

    [Header("------Variables of Situaction--------")]
    public bool ledgeClimbStatus;
    public bool boss_one_Death; //provisorio, alterar quando tiver um boss morto de vdd
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    public StatsPlayer Stats { get; private set; }
    
    public InteractableDetector InteractableDetector { get; private set; }
    #endregion

    #region Other Variables         

    private Vector2 workspace;

    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        
        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);

        Stats = Core.GetCoreComponent<StatsPlayer>();
        InteractableDetector = Core.GetCoreComponent<InteractableDetector>();
        
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", primaryWeapon, CombatInputs.primary);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", secondaryWeapon, CombatInputs.secondary);
        PlayerStunState = new PlayerStunState(this, StateMachine, playerData, "stun");
        PlayerSleepState = new PlayerSlepState(this, StateMachine, playerData, "sleep");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        InputHandler.OnInteractInputChanged += InteractableDetector.TryInteract;
        
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();

        Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;
        Stats.Sleep.OnCurrentValueZero += HandleSleepCurrentValueZero;

        StateMachine.Initialize(IdleState);

        CrouchIdleState.noCeiling = true;

        playerData.wallJumpVelocity = playerDataWallJumpVelocity;
        playerData.wallJumpTime = playerDataWallJumpTime;

        simpleWind = GameObject.FindGameObjectWithTag("Wind").GetComponent<SimpleWind>();   
    }

    private void InputHandler_OnEscapeInputChanged(bool obj)
    {
        throw new NotImplementedException();
    }

    private void HandlePoiseCurrentValueZero()
    {
        if ((CrouchIdleState.noCeiling || !CrouchIdleState.crouch) && (CrouchMoveState.noCeiling || !CrouchMoveState.crouch)) //verificar se está tocando, achar um jeito de tomar stun estando agachado
        {
            StateMachine.ChangeState(PlayerStunState);
        }
    }

    private void HandleSleepCurrentValueZero()
    {
        if ((CrouchIdleState.noCeiling || !CrouchIdleState.crouch) && (CrouchMoveState.noCeiling || !CrouchMoveState.crouch)) //verificar se está tocando, achar um jeito de tomar stun estando agachado
        {
            StateMachine.ChangeState(PlayerSleepState);
        }
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        if((JumpState.amountOfJumpsLeft <= 0)) //quando o player pular, ele desativa o vento dos pes
        {
            simpleWind.feetWindAnim.SetBool("active", false);
        }

        //alterar isso aqui depois para chamar somente quando necessario (quando algo ativar o !wallJumpStatus
        if (!wallJumpStatus) 
        {
            playerData.wallJumpVelocity = 0f;
            playerData.wallJumpTime = 0f;
        }
        else if (wallJumpStatus)
        {
            playerData.wallJumpVelocity = playerDataWallJumpVelocity;
            playerData.wallJumpTime = playerDataWallJumpTime;
        }
        //alterar isso aqui depois para chamar somente quando necessario (quando algo ativar o !wallJumpStatus
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wind")) //esse ganha um pulo extra no wind
        {
            JumpState.ResetAmountOfJumpsLeft();
        }

        if (collision.CompareTag("Platform"))
        {
            grabStatus = false;
            ledgeClimbStatus = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {          
            ledgeClimbStatus = true;

            if (boss_one_Death)//alterar isso aqui para quando tiver um boss
            {
                grabStatus = true;
            }
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        Stats.Poise.OnCurrentValueZero -= HandlePoiseCurrentValueZero;
    }

    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }   

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

   
    #endregion
}
