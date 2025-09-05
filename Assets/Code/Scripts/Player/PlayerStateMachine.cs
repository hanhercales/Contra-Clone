using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private Animator animator;
    
    public AnimationClip shootClip;
    public AnimationClip shootUpClip;
    public AnimationClip shootDownClip;
    public AnimationClip crouchShootClip;
    public AnimationClip runShootClip;
    public AnimationClip runShootUpClip;
    public AnimationClip runShootDownClip;
    public AnimationClip airShootClip;
    public AnimationClip airShootUpClip;
    public AnimationClip airShootDownClip;
    public AnimationClip hurtClip;
    public AnimationClip dieClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
    
    public void SetGrounded(bool grounded)
    {
        animator.SetBool("IsGrounded", grounded);
    }
    
    public void SetCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

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
    
    private void ResetShootLayer()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Shoot Layer"), 0f);
    }
    
    public void Hurt()
    {
        animator.Play(hurtClip.name);
    }
    
    public void Die()
    {
        animator.SetBool("IsDead", true);
        animator.Play(dieClip.name);
    }
}
