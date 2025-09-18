using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Crouch,
        Fall,
        Hurt,
        Die,
        Shoot,
        ShootUp,
        ShootDown,
        RunShoot,
        RunShootUp,
        RunShootDown,
        AirShoot,
        AirShootUp,
        AirShootDown,
        CrouchShoot
    }

    public PlayerState currentState;

    public Animator animator;
    
    public Dictionary<PlayerState, string> stateAnimations;
    
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        stateAnimations = new Dictionary<PlayerState, string>
        {
            { PlayerState.Idle, "Idle" },
            { PlayerState.Run, "Run" },
            { PlayerState.Jump, "Jump" },
            { PlayerState.Fall, "Fall" },
            { PlayerState.Crouch, "Crouch" },
            { PlayerState.Hurt, "Hurt" },
            { PlayerState.Die, "Die" },
            { PlayerState.Shoot, "Shoot" },
            { PlayerState.ShootUp, "ShootUp" },
            { PlayerState.ShootDown, "ShootDown" },
            { PlayerState.CrouchShoot, "CrouchShoot" },
            { PlayerState.RunShoot, "RunShoot" },
            { PlayerState.RunShootUp, "RunShootUp" },
            { PlayerState.RunShootDown, "RunShootDown" },
            { PlayerState.AirShoot, "AirShoot" },
            { PlayerState.AirShootUp, "AirShootUp" },
            { PlayerState.AirShootDown, "AirShootDown" }
        };
            
        ChangeState(PlayerState.Idle);
    }

    private void Update()
    {
        PlayerState newState = GetNextState();

        if (currentState != newState)
        {
            ChangeState(newState);
        }
    }

    public PlayerState GetNextState()
    {
        PlayerState highPriorityState = CheckHighPriorityState();
        if(highPriorityState != PlayerState.Idle) return highPriorityState;

        if (!playerMovement.isGrounded) return GetAirBorneState();
        else return GetGroundedState();
    }

    private PlayerState CheckHighPriorityState()
    {
        if (playerMovement.isDead) return PlayerState.Die;
        if (playerMovement.isHurt) return PlayerState.Hurt;
        return PlayerState.Idle;
    }

    private PlayerState GetAirBorneState()
    {
        if (playerMovement.isShooting) return GetAirBorneShootingState();
        else
        {
            if(playerMovement.isFastFalling) return PlayerState.Fall;
            return PlayerState.Jump;
        }
    }

    private PlayerState GetAirBorneShootingState()
    {
        if (playerMovement.isAimingUp) return PlayerState.AirShootUp;
        if (playerMovement.isAimingDown) return PlayerState.AirShootDown;
        return PlayerState.AirShoot;
    }

    private PlayerState GetGroundedState()
    {
        if (playerMovement.isCrouching) return GetCrouchingState();
        if (playerMovement.isMoving) return GetRunningState();
        if (playerMovement.isShooting) return GetStandingShootingState();
        return PlayerState.Idle;
    }

    private PlayerState GetCrouchingState()
    {
        if (playerMovement.isShooting) return PlayerState.CrouchShoot;
        return PlayerState.Crouch;
    }

    private PlayerState GetRunningState()
    {
        if (playerMovement.isShooting)
        {
            if (playerMovement.isAimingUp) return PlayerState.RunShootUp;
            if (playerMovement.isAimingDown) return PlayerState.RunShootDown;
            return PlayerState.RunShoot;
        }
        return PlayerState.Run;
    }

    private PlayerState GetStandingShootingState()
    {
        if (playerMovement.isAimingUp) return PlayerState.ShootUp;
        if (playerMovement.isAimingDown) return PlayerState.ShootDown;
        return PlayerState.Shoot;
    }

    public void ChangeState(PlayerState newState)
    {
        if(currentState == newState) return;

        currentState = newState;
        if (stateAnimations.ContainsKey(newState))
        {
            animator.Play(stateAnimations[newState]);
        }
        else
        {
            Debug.LogError("State animation not found");
        }
    }
}
