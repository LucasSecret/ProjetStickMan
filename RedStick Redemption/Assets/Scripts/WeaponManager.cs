﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager: MonoBehaviour
{
    private IEnumerator coroutine;

    private AudioSource audiosource;
    private Animator animator;
    private Transform gunFireSprite;

    private AudioClip fireSound;
    public AudioClip reloadSound;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        gunFireSprite = transform.GetChild(0);
        gunFireSprite.gameObject.SetActive(false);

        audiosource = GetComponent<AudioSource>();

        fireSound = audiosource.clip;
        audiosource.clip = reloadSound;

        if (tag == "LightWeapon")
            audiosource.loop = false;

        else
            audiosource.loop = true;
        
        animator = GetComponent<Animator>();
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
        }
    }

    public void init()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);     
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            animator.enabled = false;
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
        
        //audiosource.Stop();
        
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


}