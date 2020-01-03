using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageManager : MonoBehaviour
{
    // Start is called before the first frame update

    int bulletType;

    void Start()
    {
        bulletType = 1; // 1 = light weapon
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {



        if (col.gameObject.tag == "NPC" || col.gameObject.tag == "Arena")
        {
            Debug.LogWarning("la balle a touché un npc !");
            col.gameObject.GetComponent<NPCHealthBar>().takeDamageByBullet(bulletType);
        }
    }
}
