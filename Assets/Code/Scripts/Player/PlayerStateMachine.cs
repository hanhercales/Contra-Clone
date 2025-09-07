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
        CrouchShoot,
        AirShoot,
        AirShootUp,
        AirShootDown,
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
            { PlayerState.Crouch, "Crouch" },
            { PlayerState.Fall, "Fall" },
            { PlayerState.Hurt, "Hurt" },
            { PlayerState.Die, "Die" },
            { PlayerState.Shoot, "Shoot" },
            { PlayerState.ShootUp, "ShootUp"},
            { PlayerState.ShootDown, "ShootDown" },
            { PlayerState.RunShoot, "RunShoot" },
            { PlayerState.RunShootUp, "RunShootUp" },
            { PlayerState.RunShootDown, "RunShootDown" },
            { PlayerState.CrouchShoot, "CrouchShoot" },
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
        PlayerState highPriorityState = CheckHighPriorityStates();
        if (highPriorityState != PlayerState.Idle)
        {
            return highPriorityState;
        }
        
        // Airborne/Grounded check
        if (!playerMovement.isGrounded)
        {
            return GetAirborneState();
        }
        else
        {
            return GetGroundedState();
        }
    }

    private PlayerState CheckHighPriorityStates()
    {
        if (playerMovement.isDead) return PlayerState.Die;
        if (playerMovement.isHurt) return PlayerState.Hurt;
        return PlayerState.Idle;
    }

    private PlayerState GetAirborneState()
    {
        if (playerMovement.isShooting)
        {
            return GetAirborneShootingState();
        }
        // If not shooting
        else
        {
            if (playerMovement.rb.velocity.y < 0)
            {
                return PlayerState.Fall;
            }
            else
            {
                return PlayerState.Jump;
            }
        }
        
    }

    private PlayerState GetAirborneShootingState()
    {
        if (playerMovement.isShooting)
        {
            if (playerMovement.isAimingUp) return PlayerState.AirShootUp;
            if (playerMovement.isAimingDown) return PlayerState.AirShootDown;
            return PlayerState.AirShoot;
        }

        return PlayerState.Fall;
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
        if (playerMovement.isCrouching) return PlayerState.CrouchShoot;
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
        if (playerMovement.isAimingUp) return PlayerState.RunShootUp;
        if (playerMovement.isAimingDown) return PlayerState.RunShootDown;
        return PlayerState.Shoot;
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;
        
        currentState = newState;
        if (stateAnimations.ContainsKey(newState))
        {
            Debug.Log("Changing state to: " + stateAnimations[newState]);
            animator.Play(stateAnimations[newState]);
        }
        else
        {
            Debug.LogError("Animation not found for state: " + newState);
        }
    }
}