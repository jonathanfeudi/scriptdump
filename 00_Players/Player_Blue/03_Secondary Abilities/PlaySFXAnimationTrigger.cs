using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXAnimationTrigger : MonoBehaviour
{
    // Play SFX On Creation // Triggered in Animation
    void PlaySFX()
    {
        GetComponent<AudioSource>().Play();
    }
}
