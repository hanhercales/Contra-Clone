using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : IState
{
    private PlayerStateMachine player;

    public PlayerDieState(PlayerStateMachine player)
    {
        this.player = player;
    }
    
    public void EnterState()
    {
        player.animator.SetBool("IsDead", true);
        player.rb.isKinematic = true;
        player.rb.velocity = Vector2.zero;
        player.enabled = false;
    }
    
    public void UpdateState()
    {
        return;
    }
    
    public void ExitState()
    {
        return;
    }
}
