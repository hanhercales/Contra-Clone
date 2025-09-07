using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerBaseState CurrentState { get; set; } 
    public PlayerStateFactory States; 
    
    private Animator animator;
    
    public float CurrentMovementSpeed { get; private set; } 
    public bool IsGrounded { get; private set; }
    public bool IsCrouching { get; private set; }


    // Old animation clips will likely be moved into individual state classes
    // public AnimationClip shootClip;
    // public AnimationClip shootUpClip;
    // public AnimationClip shootDownClip;
    // public AnimationClip crouchShootClip;
    // public AnimationClip runShootClip;
    // public AnimationClip runShootUpClip;
    // public AnimationClip runShootDownClip;
    // public AnimationClip airShootClip;
    // public AnimationClip airShootUpClip;
    // public AnimationClip airShootDownClip;
    // public AnimationClip hurtClip;
    // public AnimationClip dieClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        States = new PlayerStateFactory(this);
        CurrentState = States.Idle();
        CurrentState.EnterState();
    }
    
    void Update()
    {
        CurrentState.UpdateState();
        CurrentState.CheckSwitchStates();
        CurrentState.HandleGravity();
    }

    public void SetPlayerSpeed(float speed)
    {
        CurrentMovementSpeed = speed;
        animator.SetFloat("Speed", speed);
    }
    
    public void SetPlayerGrounded(bool grounded)
    {
        IsGrounded = grounded;
        animator.SetBool("IsGrounded", grounded);
    }
    
    public void SetPlayerCrouching(bool isCrouching)
    {
        IsCrouching = isCrouching;
        animator.SetBool("IsCrouching", isCrouching);
    }
    
    /*
    public void Shoot(Vector2 aimDirection, float currentSpeed, bool isCrouching, bool isGrounded)
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Shoot Layer"), 1f);

        if (isGrounded)
        {
            if(isCrouching) animator.Play(crouchShootClip.name);
            else if(currentSpeed > 0)
            {
                if (aimDirection.y > 0.5f) animator.Play(runShootUpClip.name);
                else if (aimDirection.y < -0.5f) animator.Play(runShootDownClip.name);
                else animator.Play(runShootClip.name);
            }
            else
            {
                if (aimDirection.y > 0.5f) animator.Play(shootUpClip.name);
                else if (aimDirection.y < -0.5f) animator.Play(shootDownClip.name);
                else animator.Play(shootClip.name);
            }
        }
        else
        {
            if (aimDirection.y > 0.5f) animator.Play(airShootUpClip.name);
            else if (aimDirection.y < -0.5f) animator.Play(airShootDownClip.name);
            else animator.Play(airShootClip.name);
        }
        
        Invoke("ResetShootLayer", shootClip.length);
    }
    */
    
    private void ResetShootLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Shoot Layer"), 0f);
    }
    
    //
    // public void Hurt()
    // {
    //     animator.Play(hurtClip.name);
    // }
    //
    // public void Die()
    // {
    //     animator.SetBool("IsDead", true);
    //     animator.Play(dieClip.name);
    // }
    //
}