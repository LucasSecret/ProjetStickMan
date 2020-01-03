using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepsSoundManager : MonoBehaviour
{

    public AudioClip[] footStepClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AudioSource>() != null)
        {
            this.audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "floor" || col.gameObject.tag == "crate")
        {
            audioSource.clip = footStepClips[UnityEngine.Random.Range(0, footStepClips.Length)];
            audioSource.Play();
        }

    }
}
