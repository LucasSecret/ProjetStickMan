using UnityEngine;
using System.Collections;

public class NPCHealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;

    public float healthBarLength;
    private bool isDestroyed;

    public PlayerAttackEnum playerAttackEnum;

    // Use this for initialization
    void Start()
    {
        healthBarLength = Screen.width / 6;
    }

    // Update is called once per frame
    void Update()
    {
        AddjustCurrentHealth(0);

        if (isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {

        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(targetPos.x, targetPos.y + 150, 60, 20), curHealth + "/" + maxHealth);

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

    public void takeDamage(int ammount)
    {
        this.curHealth -= ammount;

        if(GetComponent<AudioSource>() != null)
        {
        GetComponent<AudioSource>().Play();
        }

    }
}
