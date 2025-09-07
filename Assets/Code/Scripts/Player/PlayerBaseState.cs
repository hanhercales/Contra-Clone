using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    protected PlayerStateMachine ctx;
    protected PlayerStateFactory factory;

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        ctx = currentContext;
        factory = playerStateFactory;
    }
    
    public abstract void EnterState(); // Entering state
    public abstract void UpdateState(); // Called every frame
    public abstract void ExitState(); // Exiting state

    public abstract void HandleGravity();
    public abstract void CheckSwitchStates();

    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();
        ctx.CurrentState = newState;
    }
}
