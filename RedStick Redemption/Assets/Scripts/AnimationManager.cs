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

    public void TriggerRunning()
    {
        anim.SetTrigger("isRunning");
    }

    public void setIdle()
    {
        anim.SetBool("isWalking", false);
    }

    public void setWalking(bool value)
    {
        anim.SetBool("isWalking", value);
    }

    public void setCrouching(bool value)
    {
        anim.SetBool("isCrouching", value);
    }

    public void setComboKickTrigger()
    {
        anim.SetTrigger("comboKick");
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
        anim.SetTrigger("punch");
    }

    public void comboPunchAnimation()
    {
        anim.Play("ComboPunch");
    }

    public void kickAnimation()
    {
        anim.SetTrigger("highKick");
    }

    public void setCrouchWalking(bool value)
    {
        anim.SetBool("isCrouchWalking", value);
    }
}
