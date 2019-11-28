using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    private float direction;
    public float speed;
    private Collider2D innerCollider;
    public bool isMoving;
    public bool isAttacking;
    public PlayerAttackEnum npcAttackType;
    public Color color;

    private AnimationManager animationManager;
    private NPCHealthBar npcHealth;

    // Start is called before the first frame update
    void Start()
    {
        animationManager = GetComponent<AnimationManager>();
        npcHealth = GetComponent<NPCHealthBar>();
        npcAttackType = GetComponent<PlayerAttackEnum>();

        direction = Random.Range(-1.0f, 1.0f);

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
        npcAttackType.PlayerAttackType = PlayerAttackEnum.PlayerAttack.kick;
        isMoving = false;
        animationManager.kickAnimation();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        

        //On récupere le collider qui rentre en collision avec un tel objet : Utilisé pour gérer les collider des membres, attributs du joueur.
        innerCollider = col.contacts[0].otherCollider;

        if (col.gameObject.tag != "floor")
        {

            if (isAttacking)
            {
                if(col.gameObject.tag == "Player")
                {
                col.gameObject.GetComponent<PlayerControllerScript>().takeDamage(npcAttackType.PlayerAttackType, 10f);
                }
                else if (col.gameObject.tag == "NPC")
                {
                    col.gameObject.GetComponent<NPCHealthBar>().takeDamage(npcAttackType.PlayerAttackType, 10f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        isAttacking = animationManager.getIsAttacking();
        if(isMoving)
        {
            if (direction > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);


                animationManager.startWalking();


                GetComponent<Transform>().Translate(GetComponent<Transform>().right * speed * Time.deltaTime * 1.0f * 1.0f);
            }

            else if (direction < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);


                animationManager.startWalking();

                GetComponent<Transform>().Translate(GetComponent<Transform>().right * -speed * Time.deltaTime * 1.0f * 1.0f);
            }



            if (direction == 0)
            {
               // animationManager.setIdle();//
                isMoving = false;
            }


            
        }

        if (npcHealth.isAttacked)
        {
            attackPlayer();
        }

    }
}
