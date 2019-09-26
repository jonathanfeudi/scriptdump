using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Upgrade_Deployable : MonoBehaviour
{
    // Player Controller
    public BluePlayerController bluePlayerController;

    // Blue Gun
    public BlueGunController blueGunController;

    // Movement
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    //======================================

    void OnEnable()
    {
        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();

        bluePlayerController.canShoot = false;  // Disable Firing Script

        // Gun Controller
        blueGunController = GameObject.FindGameObjectWithTag("Blue_Gun").GetComponent<BlueGunController>();

        blueGunController.isFiringLaser = false;

        // Move Player 
        myRigidbody = GetComponent<Rigidbody>();

        myRigidbody.velocity = Vector3.zero;    // Stop Movement

        // Disable Firing
        GetComponent<BlueFiringPrimary>().enabled = false;
    }

    //======================================

    void Update()   // State Transitions
    {
        // Cancel Upgrade

        if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            this.enabled = false;
            GetComponent<Blue_Idle>().enabled = true;
        }
    }

    // Turn Off Alt Warp Variable
    void OnDisable()
    {
        Debug.Log("EXIT");
        bluePlayerController.canShoot = true;  // Enable Firing Script
    }
}
