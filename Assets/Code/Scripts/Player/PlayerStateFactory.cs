using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        // return new PlayerIdleState(_context, this);
        return null;
    }
    
    // public PlayerBaseState Run()
    // {
    //     return new PlayerRunState(_context, this);
    // }
    //
    // public PlayerBaseState Jump()
    // {
    //     return new PlayerJumpState(_context, this);
    // }
    //
    // public PlayerBaseState Fall()
    // {
    //     return new PlayerFallState(_context, this);
    // }
    //
    // public PlayerBaseState Land()
    // {
    //     return new PlayerLandState(_context, this);
    // }
    //
    // public PlayerBaseState Shoot()
    // {
    //     return new PlayerShootState(_context, this);
    // }
}
