using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private ScoreManager scoreManager;
    public int GemValue;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
