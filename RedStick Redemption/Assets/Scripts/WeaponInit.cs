using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        transform.SetParent(null);     
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            GetComponent<Animator>().enabled = false;
            transform.SetParent(GameObject.Find("MainDroite").transform);
            transform.localPosition = new Vector2(0, 0);

            Debug.Log(transform.rotation);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            Debug.Log(transform.rotation);

            GetComponent<Collider2D>().enabled = false;
            GameObject.Find("Stickman").GetComponent<Animator>().SetFloat("GunTree", 1);
        }
    }
}
