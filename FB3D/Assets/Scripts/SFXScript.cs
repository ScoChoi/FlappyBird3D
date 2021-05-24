using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SFXScript : MonoBehaviour
{
    public AudioSource flap;

    public AudioClip flapJump;
    public static SFXScript sfxInstance;

    private void Awake() 
    {
            if (sfxInstance != null && sfxInstance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            sfxInstance = this;
    }
}
