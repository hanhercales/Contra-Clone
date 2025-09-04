using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerStateMachine player;

    public PlayerIdleState(PlayerStateMachine player)
    {
        this.player = player;
    }
    
    public void EnterState()
    {
        player.animator.SetBool("isRunning", false);
        player.animator.SetBool("isJumping", false);
        player.animator.SetBool("isShooting", false);
        player.animator.SetBool("isFalling", false);
        player.animator.SetBool("isHurting", false);
    }
    
    public void UpdateState()
    {
        if(Input.GetButton("Horizontal")) player.SwitchState(player.runState);
        if(Input.GetButton("Jump")) player.SwitchState(player.jumpState);
        else if (Input.GetButton("Crouch")) player.SwitchState(player.crouchState);
        if(Input.GetButton("Shoot")) player.animator.SetBool("isShooting", true);
        else player.animator.SetBool("isShooting", false);
    }
    
    public void ExitState()
    {
        return;
    }
}
