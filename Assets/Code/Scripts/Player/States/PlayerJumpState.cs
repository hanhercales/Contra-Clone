using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    private PlayerStateMachine player;

    public PlayerJumpState(PlayerStateMachine player)
    {
        this.player = player;
    }
    
    public void EnterState()
    {
        player.animator.SetBool("IsJumping", true);
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
    }
    
    public void UpdateState()
    {
        if (player.rb.velocity.y < 0)
        {
            player.SwitchState(player.fallState);
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        player.rb.velocity = new Vector2(horizontalInput * 5f, player.rb.velocity.y);
        
        if(Input.GetButton("Shoot")) player.animator.SetBool("isShooting", true);
        else player.animator.SetBool("isShooting", false);
    }
    
    public void ExitState()
    {
        player.animator.SetBool("IsJumping", false);
    }
}
