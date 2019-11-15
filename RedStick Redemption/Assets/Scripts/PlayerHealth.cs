using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int startingHP = 500; //Le nombre de HitPoint que possède le joueur au début
    private int currentHp;
    private bool isDamaged; // Quand le joeur recoit un coup



    private void Awake()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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


}
