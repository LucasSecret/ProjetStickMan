using UnityEngine;
using System.Collections;
using Anima2D;

public class NPCHealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;

    public float healthBarLength;
    private bool isDestroyed;
    Vector2 targetPos;

    private PlayerAttackEnum playerAttackEnum;
    private Rigidbody2D rigidbody2D;
    private Color[] currentColors;
    private GameObject arena;

    private float timerToStopAttack;
    private NpcBehavior npcBehavior;


    // Use this for initialization
    void Start()
    {
        healthBarLength = Screen.width / 6;
        rigidbody2D = GetComponent<Rigidbody2D>();
        arena = GameObject.Find("ArenaWall");
        npcBehavior = GetComponent<NpcBehavior>();
    }


    // Update is called once per frame
    void Update()
    {
        AddjustCurrentHealth(0);

        if (isDestroyed)
        {
            if (gameObject.tag == "Arena")
            {
                arena.GetComponent<Arena>().EndArena();
            }
            Destroy(gameObject);
            
        }

        
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        if(npcBehavior.isAttacked)
        {
            timerToStopAttack += Time.deltaTime;

            if(timerToStopAttack >= 3)
            {
                npcBehavior.isAttacked = false;
                Debug.LogWarning("j'arrete d'attaquer");
                timerToStopAttack = 0;
            }
        }

    }

    void OnGUI()
    {

        

        GUI.Box(new Rect(targetPos.x - (healthBarLength / 2), (Screen.height - targetPos.y) - 100, healthBarLength, 60), curHealth + "/" + maxHealth);

    }

    public void AddjustCurrentHealth(int adj)
    {
        curHealth += adj;

        if (curHealth <= 0)
        {
            curHealth = 0;
            isDestroyed = true;
            transform.Find("DeathSound").GetComponent<AudioSource>().clip = transform.Find("DeathSound").GetComponent<AudioSource>().clip;
            transform.Find("DeathSound").GetComponent<AudioSource>().volume = 1.0f;
            transform.Find("DeathSound").GetComponent<AudioSource>().Play();
        }

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;

        healthBarLength = (Screen.width / 6) * (curHealth / (float)maxHealth);


    }

    public void takeDamage(PlayerAttackEnum.PlayerAttack playerAttackType, float dir)
    {
        int ammountDamage = 0;
        npcBehavior.isAttacked = true;

        if(GetComponent<AudioSource>() != null)
        {
        GetComponent<AudioSource>().Play();
        }

        switch(playerAttackType)
        {
            case PlayerAttackEnum.PlayerAttack.punch:
                ammountDamage = 1;
                break;
            case PlayerAttackEnum.PlayerAttack.uppercut:
                rigidbody2D.AddForce(new Vector2(100f * dir, 5000f));
                ammountDamage = 4;
                break;
            case PlayerAttackEnum.PlayerAttack.kick:
                rigidbody2D.AddForce(new Vector2(1000.0f * dir, 2200f));
                ammountDamage = 5;
                break;
            case PlayerAttackEnum.PlayerAttack.lowkick: ammountDamage = 4; break;
            case PlayerAttackEnum.PlayerAttack.flyingKick: ammountDamage = 10; break;
        }

        this.curHealth -= ammountDamage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            curHealth = 0;
        }
    }
}
