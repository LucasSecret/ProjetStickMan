using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager: MonoBehaviour
{
    private IEnumerator coroutine;

    private AudioSource audiosource;

    private Transform gunFireSprite;

    public AudioClip fireSound;
    public AudioClip reloadSound;

    public Rigidbody2D bulletPrefab;

    public bool haveToLaunchBullets { get; set; }
    private bool isPicked = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        gunFireSprite = transform.GetChild(0);
        gunFireSprite.gameObject.SetActive(false);

        audiosource = GetComponent<AudioSource>();

        if (tag == "LightWeapon")
            audiosource.loop = false;

        else
            audiosource.loop = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "LoudWeapon")
        {
            resetRotation();

            Color c = gunFireSprite.GetComponent<SpriteRenderer>().color;
            c.a = (int)(Mathf.Abs(Mathf.Cos(Time.frameCount)) + 0.5f);
            gunFireSprite.GetComponent<SpriteRenderer>().color = c;

            if (haveToLaunchBullets)
                launchBullets();
        }

        if(!isPicked)
            gameObject.transform.position += new Vector3(0, Mathf.Cos(Time.frameCount * 0.1f) * 0.01f, 0);
    }

    public void init()
    {
        gameObject.SetActive(true);
        Vector2 parentPos = transform.parent.position;
        transform.SetParent(null);
        transform.position = parentPos;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
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

            else if (tag == "ShotGun")
                stickman.GetComponent<Animator>().SetFloat("LoudWeapon", 2);

            isPicked = true;
        }
    }

    public void resetRotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    public void playSound()
    {
        audiosource.Play();
        gunFireSprite.gameObject.SetActive(true);

        if(tag == "LightWeapon" || tag == "ShotGun")
        {
            coroutine = displayGunFireSpriteFor(0.05f);
            StartCoroutine(coroutine);
            launchBullets();
        }
    }


    private IEnumerator displayGunFireSpriteFor(float duration)
    {
        yield return new WaitForSeconds(duration);
        gunFireSprite.gameObject.SetActive(false);
    }

    private IEnumerator resetAudioClipAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        audiosource.clip = fireSound;
    }



    public void stopSound()
    {
        if(tag == "LightWeapon" || tag == "ShotGun")
            audiosource.loop = false;
                
        if(tag == "LoudWeapon")
            gunFireSprite.gameObject.SetActive(false);
    }

    public void playReloadSound()
    {
        audiosource.loop = false;
        audiosource.clip = reloadSound;
        audiosource.Play();
        coroutine = resetAudioClipAfter(reloadSound.length);
        StartCoroutine(coroutine);
    }

    public void launchBullets()
    {
        Rigidbody2D bullet = Instantiate(bulletPrefab, transform.GetChild(0));
        bullet.transform.localPosition = new Vector2(0, 0);
        bullet.transform.SetParent(null);

        bullet.velocity = GameObject.Find("Stickman").transform.right * 400.0f;
    }
}
