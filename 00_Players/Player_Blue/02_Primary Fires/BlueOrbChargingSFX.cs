using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOrbChargingSFX : MonoBehaviour
{
    public AudioSource chargingIntro;

    public AudioSource chargedLoop;

    public AudioSource chargedShot;

    public AudioSource chargedShotMoving;

    public bool _fired = false;

    // Start is called before the first frame update
    void Start()
    {
        chargingIntro.Play();
        chargedLoop.PlayDelayed(chargingIntro.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Joystick1Button7))
        {
            chargingIntro.Stop();

            chargedLoop.Stop();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button7) & _fired == false)
        {
            _fired = true;
            chargedShot.Play();
            chargedShotMoving.PlayDelayed(chargedShot.clip.length);
        }
    }
}
