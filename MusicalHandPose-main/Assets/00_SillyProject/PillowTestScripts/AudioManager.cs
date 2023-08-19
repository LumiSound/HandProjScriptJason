using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public AudioClip soundClip;
    public float cooldownTime = 1.0f; 
    private bool canPlaySound = true; 
  

    public void PlaySoundEffect()
    {
       
        if (canPlaySound)
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            canPlaySound = false; // once play set to false and wait for Reset
            StartCoroutine(ResetSoundCooldown()); // keep going
        }
    }

    
    private IEnumerator ResetSoundCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canPlaySound = true;
    }
}
