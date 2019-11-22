using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager: MonoBehaviour
{

    public bool haveToFreezeRotation { get; set; }

    private Transform gunFireSprite;


    // Start is called before the first frame update
    void Start()
    {
        gunFireSprite = transform.GetChild(0);
        gunFireSprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        resetRotation();
        
        Color c = gunFireSprite.GetComponent<SpriteRenderer>().color;

        c.a = (int) (Mathf.Abs(Mathf.Cos(Time.frameCount)) + 0.5f);

        Debug.Log(c.a);
        gunFireSprite.GetComponent<SpriteRenderer>().color = c;
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

            transform.localRotation = Quaternion.Euler(0, 0, 0);

            GetComponent<Collider2D>().enabled = false;

            GameObject stickman = GameObject.Find("Stickman");
            stickman.GetComponent<Animator>().SetFloat("GunTree", 1);
            stickman.GetComponent<PlayerControllerScript>().HasGun = true;
            stickman.GetComponent<PlayerControllerScript>().weapon = this.gameObject;

            if (tag == "LoudWeapon")
                stickman.GetComponent<Animator>().SetFloat("LoudWeapon", 1);
        }
    }

    public void resetRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    public void playSound()
    {
        GetComponent<AudioSource>().Play();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void stopSound()
    {
        GetComponent<AudioSource>().Stop();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
