using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IState
{
    private PlayerStateMachine player;

    public PlayerFallState(PlayerStateMachine player)
    {
        this.player = player;
    }
    
    public void EnterState()
    {
        player.animator.SetBool("isFalling", true);
    }
    
    public void UpdateState()
    {
        bool isGrounded = Physics2D.OverlapCircle(player.transform.position, 0.2f, player.groundLayer);

        if (isGrounded)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                player.SwitchState(player.runState);
            }
            else
            {
                player.SwitchState(player.idleState);
            }
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        player.rb.velocity = new Vector2(horizontalInput * 5f, player.rb.velocity.y);
        
        if(Input.GetButton("Shoot")) player.animator.SetBool("isShooting", true);
        else player.animator.SetBool("isShooting", false);
    }
    
    public void ExitState()
    {
        player.animator.SetBool("isFalling", false);
    }
}
