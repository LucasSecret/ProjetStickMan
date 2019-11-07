using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbable : MonoBehaviour {
    private BoxCollider2D PlayerCol;
    private float PlayerColHeight;
    private float PlayerColWidth;
    private bool climb = false;
    public bool Climb
    {
        get { return climb; }
        set { climb = value; }
    }
    private PlayerControllerScript PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        PlayerColHeight = PlayerCol.bounds.size.y;
        PlayerColWidth = PlayerCol.bounds.size.x; 

        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (!PlayerController.IsOnGround && col.gameObject.tag == "Player")
        {
            Vector2 position = transform.position;
            Vector2 wallPos = col.transform.position;
            float wallColHeight = col.gameObject.GetComponent<BoxCollider2D>().bounds.size.y;
            if ((position.y + PlayerColHeight / 2) <= (wallPos.y + wallColHeight / 2))
            {
                climb = true;
            }
            Debug.Log(climb);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
