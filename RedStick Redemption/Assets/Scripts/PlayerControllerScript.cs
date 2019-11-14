using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float jumpForce;
    public float hitStrenghtMultiplier = 1.0f; //Le multiplicateur de force que le joueur possède
    public float climbForce;

    protected bool isJumping;
    protected bool isOnGround;
    public bool IsOnGround
    {
        get { return isOnGround; }
        set { isOnGround = value; }
    }

    protected bool isCrouching;
    protected bool isRunning;
    protected bool isAttacking;
    protected bool isClimbing;
    protected bool goUp;

    /* Notre réference vers notre manager d'animation : BIEN SEPARER LE PLAYER CONTROLLER DE LANIMATION CONTROLLER !*/
    private AnimationManager animationManager;
    private PlayerHealth playerHealth;
    private Collider2D innerCollider;
    private float colorDelta;
    private Rigidbody2D rigidbody2D;
    private Transform transform;
    private AudioSource audioSource;
    private float sprintMultiplier;
    private float crouchMultiplier;
    private float gravityScale;



    public AudioClip[] audioClips;




    // Start is called before the first frame update
    void Start()
    {

        if (GetComponent<Rigidbody2D>() != null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            gravityScale = rigidbody2D.gravityScale;

        }

        if (GetComponent<Transform>() != null)
        {
            transform = GetComponent<Transform>();
        }

        if (GetComponent<AnimationManager>() != null)
        {
            this.animationManager = GetComponent<AnimationManager>();
        }

        if (GetComponent<PlayerHealth>() != null)
        {
            this.playerHealth = GetComponent<PlayerHealth>();
        }
        if (GetComponent<AudioSource>() != null)
        {
            this.audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor") //Doit être remplacé par un tag platform 
        {
            isOnGround = true;
            animationManager.getOnGroundAnimation();

        }

        else if ((col.gameObject.tag == "climbable"))
        {
            isClimbing = true;
        }

        //On récupere le collider qui rentre en collision avec un tel objet : Utilisé pour gérer les collider des membres, attributs du joueur.
        innerCollider = col.contacts[0].otherCollider;

        if (col.gameObject.tag != "floor" && col.gameObject.tag == "Caisse")
        {
            if (innerCollider.gameObject.name == "MoletG" || innerCollider.gameObject.name == "MoletD" || innerCollider.gameObject.name == "AvBrasG" || innerCollider.gameObject.name == "AvBrasD")
            {
                if (animationManager.getIsAttacking())
                {
                    col.gameObject.GetComponent<CaisseControler>().takeDamage(50);

                    audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
                    audioSource.Play();
                }
            }
        }
    }


    void OnCollisionExit2D(Collision2D col)
    {
        if ((col.gameObject.tag == "climbable"))
        {
            isClimbing = false;
        }
    }

    private void handleMoletDroit(Transform childOfRightLeg)
    {

    }

    private void handleCuisseDroite(Transform childOfRightLeg)
    {

    }

    private void handleMoletGauche(Transform childOfLeftLeg)
    {
        Debug.Log("Handle handleMoletGauche !");
    }

    private void handleCuisseGauche(Transform childOfLeftLeg)
    {
        Debug.Log("Handle handleCuisseGauche !");
    }

    private void handleAvantBrasDroit(Transform child4)
    {
        Debug.Log("Handle handleAvantBrasDroit !");
    }

    private void handleBrasDroit(Transform child4)
    {
        Debug.Log("Handle handleBrasDroit !");

    }

    private void handleAvantBrasGauche(Transform child4)
    {
        Debug.Log("Handle handleAvantBrasGauche !");
    }

    private void handleBrasGauche(Transform child4)
    {
        Debug.Log("Handle handleBrasGauche !");
    }

    private void handleHead(Transform child3)
    {
        Debug.Log("Handle handleHead !");
    }

    void Update()
    {

        float direction = Input.GetAxis("Horizontal");

        if (direction > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

            if (!isCrouching)
                animationManager.startWalking();
            

            else
                animationManager.startCrouchWalking();


            GetComponent<Transform>().Translate(GetComponent<Transform>().right * 10.05f * Time.deltaTime * sprintMultiplier * crouchMultiplier);
        }

        else if (direction < 0 )
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

            if (!isCrouching)
                animationManager.startWalking();

            else
                animationManager.startCrouchWalking();
            

            GetComponent<Transform>().Translate(GetComponent<Transform>().right * (-10.05f) * Time.deltaTime * sprintMultiplier * crouchMultiplier);
        }

        

        if (direction == 0)
        {
            if (isCrouching)
                animationManager.stopCrouchWalking();

            else animationManager.setIdle();
        }


        if (Input.GetAxis("Vertical") < 0)
        {
            animationManager.setCrounching();
            isCrouching = true;

        }

        else if (Input.GetAxis("Vertical") > 0)
        {
            goUp = true;
        }

        else
        {
            goUp = false;
            animationManager.setNotCrounching();
            animationManager.stopCrouchWalking();
            isCrouching = false;
        }

        HandleInput();

        if (isRunning)
            sprintMultiplier = 2.0f;
        
        else
            sprintMultiplier = 1.0f;
       

        if (isCrouching)
            crouchMultiplier = 0.5f;
        
        else
            crouchMultiplier = 1.0f;



        if (!goUp)
        {
            rigidbody2D.gravityScale = gravityScale;
            animationManager.stopClimbing();
        }

        else if (goUp && isClimbing)
        {
            animationManager.startClimbing();
            transform.position = transform.position + new Vector3(0, 1, 0) * climbForce;
            rigidbody2D.gravityScale = 0.0f;
        }
    }



    void FixedUpdate()
    {
        if (isJumping)
        {
            animationManager.JumpAnimation();
            this.rigidbody2D.AddForce(new Vector2(0.0f, jumpForce));

            isOnGround = false;
            isJumping = !isJumping;

        }

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
            animationManager.startRunning();
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround)
        {
            isRunning = false;
            animationManager.stopRunning();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("j'ai frapper au poingts en bas");
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("j'ai frapper au poingts en haut");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("j'ai frapper au poingts a droite");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("j'ai frapper au poingts a gauche");
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("j'ai frapper au poingts normal");
            animationManager.punchAnimation();

        }





        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("j'ai frapper en bas");
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("j'ai frapper en haut");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("j'ai frapper a droite");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("j'ai frapper a gauche");
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
            animationManager.uppercutAnimation();

        if (Input.GetKeyDown(KeyCode.L))
            animationManager.lowKickAnimation();

        if (Input.GetKeyDown(KeyCode.K) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("j'ai frapper normal");
            animationManager.kickAnimation();

        }



    }
}

