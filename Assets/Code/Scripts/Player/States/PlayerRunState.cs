using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    private PlayerStateMachine player;

    public PlayerRunState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.animator.SetBool("isRunning", true);
    }
    
    public void UpdateState()
    {
        float moving = Input.GetAxis("Horizontal");
        if(Mathf.Abs(moving) > 0.1f)
        {
            player.rb.velocity = new Vector2(moving * player.moveSpeed, player.rb.velocity.y);
            if(moving > 0) player.transform.localScale = new Vector3(1, 1, 1);
            else if(moving < 0) player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            player.SwitchState(player.idleState);
        }
        
        if(Input.GetButton("Jump")) player.SwitchState(player.jumpState);
        
        if(Input.GetButton("Shoot")) player.animator.SetBool("isShooting", true);
        else player.animator.SetBool("isShooting", false);
    }
    
    public void ExitState()
    {
        player.animator.SetBool("isRunning", false);
    }
}
