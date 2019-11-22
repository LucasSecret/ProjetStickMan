using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private ScoreManager scoreManager;
    public int GemValue;
    public float rand;
    System.Random alea = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
        
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = transform.position + new Vector3 (0,1,0) * Mathf.Cos(0.05f*Time.frameCount + rand)* 0.015f;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            scoreManager.AddGem(GemValue);
            Destroy(gameObject);
        }
    }
}
