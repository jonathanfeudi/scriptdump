using UnityEngine;
using System.Collections;

public class BlueGunSoundSFX : MonoBehaviour
{
    public AudioSource AudioSource_01;
    public AudioSource AudioSource_02;


    // Audio 1
    public void play_sfx_01()
    {
        AudioSource_01.Play();
    }

    public void stop_sfx_01()
    {
        AudioSource_01.Stop();
    }



    // Audio 2
    public void play_sfx_02()
    {
        AudioSource_02.Play();
    }
}