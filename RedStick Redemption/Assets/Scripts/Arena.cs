using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public bool arenaStart = false;
    Rigidbody2D wall;

    void Start()
    {
        wall = GetComponent<Rigidbody2D>();
        wall.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            GetComponent<Rigidbody2D>().gravityScale = 200;
        }
    }

    public void EndArena()
    {
        Destroy(gameObject);
    }
}
