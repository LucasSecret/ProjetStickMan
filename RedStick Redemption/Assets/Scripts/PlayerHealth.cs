using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int startingHP = 500; //Le nombre de HitPoint que possède le joueur au début
    private int currentHp;
    private bool isDamaged; // Quand le joeur recoit un cou
    public float healthBarLength;
    Vector2 targetPos;



    private void Awake()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBarLength = Screen.width / 6;
        currentHp = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        healthBarLength = (Screen.width / 6) * (currentHp / (float)startingHP);
    }

    void OnGUI()
    {



        GUI.Box(new Rect(targetPos.x - (healthBarLength / 2), (Screen.height - targetPos.y) - 100, healthBarLength, 60), currentHp + "/" + startingHP);

    }

    public void TakeDamage(int amount)
    {
        isDamaged = true;

        currentHp -= amount;

        currentHp = Mathf.Clamp(currentHp, 0, startingHP);

        if(currentHp <= 0)
        {
            setDead();
        }

    }

    void setDead()
    {
        Application.Quit();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DeathZone")
        {
            currentHp = 0;
            setDead();
        }
    }
}
