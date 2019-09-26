using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWalkingSFX : MonoBehaviour
{
    public BluePlayerController BlueObject;  // Object to follow

    // Start is called before the first frame update
    void PlayWalkSFX()
    {
        // IDLE to RUNNING
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0)
            ||
           (Input.GetAxisRaw("Key_X") != 0) || (Input.GetAxisRaw("Key_Y") != 0))
        {
            GetComponent<AudioSource>().Play();
        }
    }    
}
