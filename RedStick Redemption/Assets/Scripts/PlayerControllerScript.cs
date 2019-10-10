using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float jumpForce = 5000f;

    private Rigidbody2D rigidbody2D;
    private Transform transform;
    protected bool isJumping;
    protected bool isOnGround;
    protected bool isCrouching;
    protected bool isRunning;
    /* Notre réference vers notre manager d'animation : BIEN SEPARER LE PLAYER CONTROLLER DE LANIMATION CONTROLLER !*/
    private AnimationManager animationManager;
    
    // Start is called before the first frame update
    void Start()
    {

        if (GetComponent<Rigidbody2D>() != null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        if (GetComponent<Transform>() != null)
        {
            transform = GetComponent<Transform>();
        }

        this.animationManager = GetComponent<AnimationManager>();


        this.rigidbody2D.gravityScale = 1.50f;
    }

    void Update()
    {

        float direction = Input.GetAxis("Horizontal");

        if (direction > 0 && !isCrouching)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponent<Transform>().Translate(GetComponent<Transform>().right * 10.05f  * Time.deltaTime);
            animationManager.setCrouchWalking(false);
            animationManager.setWalking(true);
        }
        else if (direction < 0 && !isCrouching)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            GetComponent<Transform>().Translate(GetComponent<Transform>().right * (-10.05f) * Time.deltaTime);
            animationManager.setCrouchWalking(false);
            animationManager.setWalking(true);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            animationManager.setCrouching(true);
            isCrouching = true;
        }
        else
        {
            animationManager.setCrouching(false);
            animationManager.setCrouchWalking(false);
            isCrouching = false;
        }


        if (direction != 0 && isCrouching)
        {
            GetComponent<Transform>().Translate(GetComponent<Transform>().right * (5.05f) * Time.deltaTime);
            animationManager.setCrouchWalking(true);
            animationManager.setCrouching(false);
            animationManager.setWalking(false);
        }

        if (direction == 0 && !isCrouching)
        {
            animationManager.setIdle();
        }


        

        HandleInput();
    }



    void FixedUpdate()
    {
        if (isJumping)
        {
            animationManager.JumpAnimation();
            this.rigidbody2D.AddForce(new Vector2(0.0f, jumpForce));

            isOnGround = false;
            isJumping = !isJumping;

            Debug.Log("jai sauté");
            
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") //Doit être remplacé par un tag platform 
        {
            isOnGround = true;
            animationManager.getOnGroundAnimation();
        }
        Debug.Log("je suis au sol");
    }


    void HandleInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            animationManager.TriggerTakeOff();
            isJumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround)
        {
            animationManager.TriggerCrouching();
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
        {
            animationManager.TriggerRunning();
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround)
        {
            isRunning = false;
            animationManager.resetTrigger("isRunning");
        }

        if (Input.GetKeyDown(KeyCode.P))
            animationManager.punchAnimation();

        if (Input.GetKeyDown(KeyCode.K))
            animationManager.kickAnimation();

        if (Input.GetKeyDown(KeyCode.O))
            animationManager.comboPunchAnimation();

        if (Input.GetKeyDown(KeyCode.J))
            animationManager.setComboKickTrigger();
    }
}

