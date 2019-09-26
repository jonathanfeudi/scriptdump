using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteractDeployable : MonoBehaviour
{
    public BlueDeployableInteract _deploy;
    
    // Play SFX On Creation // Triggered in Animation
    void canInteractDeployable()
    {
        Debug.Log("DEPLOY END ANIMATION");
        _deploy.counterCanInteract = 0;
    }
}