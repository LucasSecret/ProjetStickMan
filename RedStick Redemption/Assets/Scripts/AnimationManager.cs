using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    protected Animator anim;

    private PlayerControllerScript playerControllerScript;


    void Start()
    {

        this.anim = GetComponent<Animator>();
        this.playerControllerScript = GetComponent<PlayerControllerScript>();
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isCrouching", false);
    }

    void Update()
    {

    }

    /*
     * @Deprecated
    public Animator GetAnimator()
    {
        return this.anim;
    }*/

    public void JumpAnimation()
    {
        anim.SetBool("isJumping", true);
        anim.SetBool("isWalking", false);
    }

    public void getOnGroundAnimation()
    {
        anim.SetBool("isJumping", false);
    }

    public void TriggerTakeOff()
    {
        anim.SetTrigger("takeOff");
    }

    public void TriggerCrouching()
    {
        anim.SetTrigger("isCrouching");
    }

    public void startRunning()
    {
        anim.SetBool("isRunning", true);
    }

    public void stopRunning()
    {
        anim.SetBool("isRunning", false);
    }

    public void setIdle()
    {
        anim.SetBool("isWalking", false);
    }

    public void setWalking()
    {
        anim.SetBool("isWalking", true);
    }

    public void setCrounching()
    {
        anim.SetBool("isCrouching", true);
    }

    public void setNotCrounching()
    {
        anim.SetBool("isCrouching", false);
    }


    public void resetTrigger(string animName)
    {
        if (animName != null && animName.Length > 0)
        {
            anim.ResetTrigger(animName);
        }
    }

    public void punchAnimation()
    {
        anim.SetBool("punch", true);
       //anim.Play("Punch");
    }

    public bool getIsAttacking()
    {
        return anim.GetBool("punch") || anim.GetBool("kick");
    }
    public void kickAnimation()
    {
        anim.SetBool("kick", true);
    }
    
    public void setAnimationState(string animName)
    {
        anim.SetBool(animName, false);
    }

}
