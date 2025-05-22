using Bardent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<bool> OnInteractInputChanged;

    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    public bool[] AttackInputs { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private Player player;

    public bool gamePaused;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];

        player = GetComponent<Player>();

        cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();

        if(Time.timeScale == 0)
        {
            gamePaused = true;
        }
        else
        {
            gamePaused = false;
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if( !gamePaused )
        {
            if (context.started)
            {
                OnInteractInputChanged?.Invoke(true);
                return;
            }

            if (context.canceled)
            {
                OnInteractInputChanged?.Invoke(false);
            }
        }

    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (!gamePaused)
        {
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.primary] = true;
            }

            if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.primary] = false;
            }
        }

    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {

        if (!gamePaused)
        {
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.secondary] = true;
            }

            if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.secondary] = false;
            }
        }

    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(!gamePaused)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);
        }    
        
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!gamePaused)
        {
            if (context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }

    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {

        if (!gamePaused)
        {
            if (context.started && player.grabStatus) //aqui eu posso definir alguma bool para que ele nao consiga grudar na parede
            {
                GrabInput = true;
            }

            if (context.canceled && player.grabStatus) //aqui eu posso definir alguma bool para que ele nao consiga grudar na parede
            {
                GrabInput = false;
            }
        }

    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (!gamePaused)
        {
            if (context.started && player.dashStatus) //aqui eu posso definir alguma bool para que ele nao consiga dar dash
            {
                DashInput = true;
                DashInputStop = false;
                dashInputStartTime = Time.time;
            }
            else if (context.canceled && player.dashStatus) //aqui eu posso definir alguma bool para que ele nao consiga dar dash
            {
                DashInputStop = true;
            }
        }

    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {

        if (!gamePaused)
        {
            RawDashDirectionInput = context.ReadValue<Vector2>();

            if (playerInput.currentControlScheme == "Keyboard")
            {
                RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
            }

            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }

    }

    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;

    /// <summary>
    /// Used to set the specific attack input back to false. Usually passed through the player attack state from an animation event.
    /// </summary>
    public void UseAttackInput(int i) => AttackInputs[i] = false;

    private void CheckJumpInputHoldTime()
    {
        if (!gamePaused)
        {
            if (Time.time >= jumpInputStartTime + inputHoldTime)
            {
                JumpInput = false;
            }
        }

    }

    private void CheckDashInputHoldTime()
    {
        if(!gamePaused)
        {
            if (Time.time >= dashInputStartTime + inputHoldTime)
            {
                DashInput = false;
            }
        }

    }
}

public enum CombatInputs
{
    primary,
    secondary
}
