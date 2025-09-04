using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IState
{
    private PlayerStateMachine player;
    private float hurtDuration = 0.5f;
    private float timer;

    public PlayerHurtState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.animator.SetBool("isHurting", true);
        player.rb.velocity = Vector2.zero;
        timer = hurtDuration;
    }
    
    public void UpdateState()
    {
        if(timer > 0) timer -= Time.deltaTime;
        else player.SwitchState(player.idleState);
    }

    public void ExitState()
    {
        player.animator.SetBool("isHurting", false);
    }
}
