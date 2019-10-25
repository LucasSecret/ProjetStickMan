using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource audioTheme;

    private static MusicController instance = null;
    public static MusicController Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        audioTheme = GetComponent<AudioSource>();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource getAudioTheme()
    {
        return this.audioTheme;
    }
}
