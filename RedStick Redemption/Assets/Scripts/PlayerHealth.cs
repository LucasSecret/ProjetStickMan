using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int startingHP = 500; //Le nombre de HitPoint que possède le joueur au début
    private int currentHp;
    private bool isDamaged; // Quand le joeur recoit un coup

    public Slider healthSlider;                                 // Reference to the UI's health bar.
                                                                //public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
                                                                //public AudioClip deathClip;                                 // The audio clip to play when the player dies.
                                                                //public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
                                                                // public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    public Text HealthText;



    private void Awake()
    {
        currentHp = startingHP;
        healthSlider.maxValue = startingHP;
        healthSlider.value = startingHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "HEALTH : " + currentHp.ToString();
    }

    public void TakeDamage(int amount)
    {
        isDamaged = true;

        currentHp -= amount;

        currentHp = Mathf.Clamp(currentHp, 0, startingHP);

        healthSlider.value = currentHp;

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
