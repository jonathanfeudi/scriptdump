using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueItemSelectionManager : MonoBehaviour
{
    public BluePlayerController bluePlayer;

    public BlueGunController blueGun;

    public BlueAudioManager blueAudio;

    public bool dpadPressed = false;

    public int itemIndexMAX = 5;

    public int itemIndex;

    // Start is called before the first frame update
    void Start()
    {
        itemIndexMAX = 5;

        itemIndex = 1;

        blueAudio = bluePlayer.GetComponent<BlueAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Select Items
        if (bluePlayer.canShoot == true & bluePlayer.GetComponent<Blue_Warp>().enabled == false & bluePlayer.GetComponent<Blue_Damaged>().enabled == false & bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled == false)
        {
            // LEFT 
            if ((Input.GetAxis("DPad_X") > 0) & (dpadPressed == false))
            {
                itemIndex += 1;
                blueAudio.AudioSource_05.Play();
                dpadPressed = true;
            }

            // LEFT
            if ((Input.GetAxis("DPad_X") < 0) & (dpadPressed == false))
            {
                itemIndex -= 1;
                blueAudio.AudioSource_05.Play();
                dpadPressed = true;
            }

            // UP
            if ((Input.GetAxis("DPad_Y") > 0) & (dpadPressed == false))
            {
                blueAudio.AudioSource_06.Play();
                dpadPressed = true;
            }

            // DOWN
            if ((Input.GetAxis("DPad_Y") < 0) & (dpadPressed == false))
            {
                itemIndex = 1;
                blueAudio.AudioSource_07.Play();
                dpadPressed = true;
            }
        }

        // Cycle Through Item Index MAX and MIN
        if (itemIndex > itemIndexMAX)
        {
            itemIndex = 1;
        }

        if (itemIndex < 1)
        {
            itemIndex = itemIndexMAX;
        }

        // Reset D-PAD Press and Reset Cycle to First Item
        if (Input.GetAxis("DPad_X") == 0 & (Input.GetAxis("DPad_Y") == 0))
        {
            dpadPressed = false;
        }
    }
}
