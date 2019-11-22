using UnityEngine;
using System.Collections;
using Anima2D;

public class NPCHealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;

    public float healthBarLength;
    private bool isDestroyed;
    public bool isAttacked;
    Vector2 targetPos;

    private PlayerAttackEnum playerAttackEnum;
    private Rigidbody2D rigidbody2D;
    private Color[] currentColors;
    private GameObject arena;
    

    // Use this for initialization
    void Start()
    {
        healthBarLength = Screen.width / 6;
        rigidbody2D = GetComponent<Rigidbody2D>();
        arena = GameObject.Find("ArenaWall");
        Component[] SpriteMesh = GetComponentsInChildren<Anima2D.SpriteMeshInstance>();


        foreach (Anima2D.SpriteMeshInstance spritemesh in SpriteMesh)
        {
            
        }
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
    }

    void OnGUI()
    {

        

        GUI.Box(new Rect(targetPos.x - (healthBarLength / 2), (Screen.height - targetPos.y) - 100, healthBarLength, 60), curHealth + "/" + maxHealth);

    }

    public void AddjustCurrentHealth(int adj)
    {
        curHealth += adj;

        if (curHealth < 0)
        {
            curHealth = 0;
            isDestroyed = true;
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
        isAttacked = true;

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
                rigidbody2D.AddForce(new Vector2(3000.0f * dir, 2200f));
                ammountDamage = 5;
                break;
            case PlayerAttackEnum.PlayerAttack.lowkick: ammountDamage = 4; break;
            case PlayerAttackEnum.PlayerAttack.flyingKick: ammountDamage = 10; break;
        }

        this.curHealth -= ammountDamage;
    }
}
