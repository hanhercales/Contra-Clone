using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : IState
{
    private PlayerStateMachine player;
    private Vector2 originalColliderSize;
    private Vector2 crouchColliderSize = new Vector2(1f, 0.5f);

    public PlayerCrouchState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.animator.SetBool("isCrouching", true);
        originalColliderSize = player.playerCollider.size;
        player.playerCollider.size = crouchColliderSize;
    }
    
    public void UpdateState()
    {
        if(Input.GetButtonUp("Crouch")) player.SwitchState(player.idleState);
        
        if(Input.GetButton("Shoot")) player.animator.SetBool("isShooting", true);
        else player.animator.SetBool("isShooting", false);
    }
    
    public void ExitState()
    {
        player.animator.SetBool("isCrouching", false);
        player.playerCollider.size = originalColliderSize;
    }
}
