using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D playerCollider;
    
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchSpeed = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    public IState currentState;
    
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerCrouchState crouchState;
    public PlayerFallState fallState;
    public PlayerHurtState hurtState;
    public PlayerDieState dieState;

    private void Awake()
    {
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        jumpState = new PlayerJumpState(this);
        crouchState = new PlayerCrouchState(this);
        fallState = new PlayerFallState(this);
        hurtState = new PlayerHurtState(this);
        dieState = new PlayerDieState(this);
    }

    private void Start()
    {
        SwitchState(idleState);
    }

    private void Update()
    {
        if(currentState != null)
            currentState.UpdateState();
    }
    
    public void SwitchState(IState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }
}
