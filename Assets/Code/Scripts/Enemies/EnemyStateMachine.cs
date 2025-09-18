using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyStateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Combat,
        //Die
    }
    
    public EnemyState currentState;

    public Animator animator;

    public Dictionary<EnemyState, string> stateAnimations;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        stateAnimations = new Dictionary<EnemyState, string>
        {
            { EnemyState.Idle, "Idle" },
            { EnemyState.Combat, "Combat"},
            // { EnemyState.Die, "Die"}
        };

        ChangeState(EnemyState.Idle);
    }
    
    private void Update()
    {
        EnemyState newState = GetNextState();

        if (currentState != newState)
        {
            ChangeState(newState);
        }
    }
    
    public EnemyState GetNextState()
    {
        EnemyState highPriorityState = CheckHighPriorityState();
        if(highPriorityState != EnemyState.Idle) return highPriorityState;

        if (enemy.isAttacking) return EnemyState.Combat;
        return EnemyState.Idle;
    }
    
    private EnemyState CheckHighPriorityState()
    {
        //if (enemy.isDead) return EnemyState.Die;
        return EnemyState.Idle;
    }

    public void ChangeState(EnemyState newState)
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
