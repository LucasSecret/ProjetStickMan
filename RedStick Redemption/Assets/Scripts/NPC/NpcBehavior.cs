using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    private float direction;
    public float speed;
    private Collider2D innerCollider;
    private Rigidbody2D rigidbody;
    public bool isMoving;
    public bool isAttacking;
    public bool isAttacked;

    private bool triggerAttackPlayer;

    
    private bool playerSpotted;
    private bool triggerPlayerSpotted;

    public PlayerAttackEnum npcAttackType;
    public Color color;

    private AnimationManager animationManager;
    private NPCHealthBar npcHealth;
    private PlayerControllerScript player;

    private Vector2 directionPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animationManager = GetComponent<AnimationManager>();
        npcHealth = GetComponent<NPCHealthBar>();
        npcAttackType = GetComponent<PlayerAttackEnum>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();

        direction = UnityEngine.Random.Range(-1.0f, 1.0f);

        speed = 2.0f;

        isMoving = true;

        InitStickManColor();





    }

    private void InitStickManColor()
    {
        Component[] SpriteMesh = GetComponentsInChildren<Anima2D.SpriteMeshInstance>();

        foreach (Anima2D.SpriteMeshInstance spritemesh in SpriteMesh)
        {
            Debug.Log(color.ToString());
            spritemesh.color = color;
        }
    }

    void attackPlayer()
    {
        if(!triggerAttackPlayer)
        {
            Array values = System.Enum.GetValues(typeof(PlayerAttackEnum.PlayerAttack));


            PlayerAttackEnum.PlayerAttack randomAttack = (PlayerAttackEnum.PlayerAttack)values.GetValue(UnityEngine.Random.Range(0, values.Length));

            npcAttackType.PlayerAttackType = randomAttack;
            isMoving = false;

            Debug.LogWarning("npc attack type : " + randomAttack.ToString());
            
            

            switch (npcAttackType.PlayerAttackType)
            {
                case PlayerAttackEnum.PlayerAttack.kick: animationManager.kickAnimation(); break;
                case PlayerAttackEnum.PlayerAttack.flyingKick: animationManager.startFlyingKick(); break;
                case PlayerAttackEnum.PlayerAttack.lowkick: animationManager.lowKickAnimation(); break;
                case PlayerAttackEnum.PlayerAttack.punch: animationManager.punchAnimation(); break;
                case PlayerAttackEnum.PlayerAttack.uppercut: animationManager.uppercutAnimation(); break;


            }

            triggerAttackPlayer = true;

        }




    }

    void setBackToWandering()
    {
        triggerAttackPlayer = false;
        animationManager.startWalking();
        isMoving = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //On récupere le collider qui rentre en collision avec un tel objet : Utilisé pour gérer les collider des membres, attributs du joueur.
        innerCollider = col;

        if (col.gameObject.tag != "floor")
        {

            if (isAttacking)
            {
                if(col.gameObject.tag == "Player")
                {
                col.gameObject.GetComponent<PlayerControllerScript>().takeDamage(npcAttackType.PlayerAttackType, transform.forward.z);
                }
                else if (col.gameObject.tag == "NPC")
                {
                    col.gameObject.GetComponent<NPCHealthBar>().takeDamage(npcAttackType.PlayerAttackType, transform.forward.z);
                }
            }
        }
        else
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arena")
        {
            if(Mathf.Abs(rigidbody.velocity.x) >= 4)
            {
                Debug.LogWarning("le npc s'est pris le mur !");
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        directionPlayer = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);

        //PAR LA DROITE
        if(direction > 0)
        {
            RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + 5, transform.position.y), new Vector2(direction, 0), 17f);

            if (hitRight.collider != null)
            {
                if (hitRight.transform.tag == "Player")
                {

                    playerSpotted = true;

                    if(hitRight.distance <= 0.05f)
                    {
                        animationManager.stopRunning();
                       
                    }

                }
            }


        }
        //PAR LA GAUCHE
        else if(direction < 0)
        {
             RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - 5, transform.position.y), new Vector2(direction, 0), 17f);

            if (hitLeft.collider != null)
            {
                if (hitLeft.transform.tag == "Player")
                {

                    playerSpotted = true;

                    if (hitLeft.distance <= 0.05f)
                    {
                        animationManager.stopRunning();
                        
                    }

                }
            }

        }


        isAttacking = animationManager.getIsAttacking();

        if(isMoving)
        {
            if (direction > 0)
            {
                if (playerSpotted)
                {
                   

                    if(directionPlayer.normalized.x > 0)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);

                        animationManager.startRunning();

                        GetComponent<Transform>().Translate(GetComponent<Transform>().right * speed * 8.0f * Time.deltaTime * 1.0f * 1.0f * directionPlayer.normalized);
                    }
                    else
                    {
                        direction = -direction;
                    }

                    
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);

                    animationManager.startWalking();

                    GetComponent<Transform>().Translate(GetComponent<Transform>().right * speed * Time.deltaTime * 1.0f * 1.0f);
                }



            }

            else if (direction < 0)
            {
                if (playerSpotted)
                {
                    if (directionPlayer.normalized.x < 0)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);

                        animationManager.startRunning();

                        GetComponent<Transform>().Translate(GetComponent<Transform>().right * speed * 8.0f * Time.deltaTime * 1.0f * 1.0f * directionPlayer.normalized);
                    }
                    else
                    {
                        direction = -direction;
                    }
                   
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);

                    animationManager.startWalking();

                    GetComponent<Transform>().Translate(GetComponent<Transform>().right * -speed * Time.deltaTime * 1.0f * 1.0f);
                }
            }



            if (direction == 0)
            {
               // animationManager.setIdle();//
                isMoving = false;
            }


            
        }

        if (isAttacked)
        {
           
        }

    }
}
