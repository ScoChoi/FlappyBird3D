using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestroyAudio : MonoBehaviour
{
     
    public AudioClip SweetsParade;
    public AudioClip Vordt;

    static Object instance = null;

    public void ChangeMusic()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = Vordt;
        GetComponent<AudioSource>().volume = 0.5f;
        GetComponent<AudioSource>().Play();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy (this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad (this.gameObject);
        }
    }
     
    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
