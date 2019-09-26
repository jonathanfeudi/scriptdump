using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Damaged : MonoBehaviour
{
    // Player Controller
    private BluePlayerController bluePlayerController;

    // Camera 
    private Camera mainCamera;

    // Movement
    private Rigidbody myRigidbody;

    public Vector3 moveVelocity;

    // KnockBack
    public float knockBackForce;
    public float knockBackTime;
    public float knockBackCounter;

    public Vector3 direction;

    // Damaged SFX
    public AudioClip damagedSFX;

    //======================================
        void OnEnable()
    {
        Debug.Log("DAMAGED_State");

        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();

        // Disable Firing
        GetComponent<BlueFiringPrimary>().enabled = false;

        // Move when Knocked Back
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        // Play DMG SFX
        AudioSource.PlayClipAtPoint(damagedSFX, transform.position);
    }

    //======================================

    // KnockBack
    public void Knockback(Vector3 direction)
    {
        // Set the Timer
        knockBackCounter = knockBackTime;
    }

    void Update()
    {
        // Knock Back Player and Subtract from Knockback Timer
        if (knockBackCounter > 0)
        {
            // Knock Back Direction and Force
            moveVelocity = direction * knockBackForce;
            // Countdown to Stop Knockback
            knockBackCounter -= Time.deltaTime;
        }

        if (knockBackCounter <= 0) // knockBackCounter resets player back to IDLE
        {
            this.enabled = false;
            GetComponent<Blue_Idle>().enabled = true;
        }

        // Turn Off Laser
        bluePlayerController.theGun.isFiringLaser = false;
    }

    //======================================

    void FixedUpdate()  // Move the Rigid Body
    {
        myRigidbody.velocity = moveVelocity;
    }
}


