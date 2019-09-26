using UnityEngine;
using System.Collections;

public class BlueAudioManager : MonoBehaviour
{
    public AudioSource AudioSource_01;
    public AudioSource AudioSource_02;
    public AudioSource AudioSource_03;
    public AudioSource AudioSource_04;
    public AudioSource AudioSource_05;
    public AudioSource AudioSource_06;
    public AudioSource AudioSource_07;

    // Audio 1 // Return Deploy
    public void play_sfx_01()
    {
        AudioSource_01.Play();
    }

    public void stop_sfx_01()
    {
        AudioSource_01.Stop();
    }


    // Audio 2 // Upgrade Presses
    public void play_sfx_02()
    {
        AudioSource_02.Play();
    }

    public void stop_sfx_02()
    {
        AudioSource_02.Stop();
    }

    // Audio 3 // Upgrade Confirmed
    public void play_sfx_03()
    {
        AudioSource_03.Play();
    }

    public void stop_sfx_03()
    {
        AudioSource_03.Stop();
    }

    // Audio 4 // Upgrade Missed Timing
    public void play_sfx_04()
    {
        AudioSource_04.Play();
    }

    public void stop_sfx_04()
    {
        AudioSource_04.Stop();
    }

    // Audio 5 // Item Change
    public void play_sfx_05()
    {
        AudioSource_05.Play();
    }

    public void stop_sfx_05()
    {
        AudioSource_05.Stop();
    }

    // Audio 6 // Item Selected
    public void play_sfx_06()
    {
        AudioSource_06.Play();
    }

    public void stop_sfx_06()
    {
        AudioSource_06.Stop();
    }

    // Audio 7 // Item Reset
    public void play_sfx_07()
    {
        AudioSource_07.Play();
    }

    public void stop_sfx_07()
    {
        AudioSource_07.Stop();
    }
}