using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInstrument : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public void Play()
    {
        audioSource.PlayOneShot(audioClip);
        Debug.Log("Play audio clip");
    }
    
}
