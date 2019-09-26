using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Roll : MonoBehaviour
{
    // Player Controller
    private BluePlayerController bluePlayerController;

    // Movement
    private Rigidbody myRigidbody;

    public Vector3 moveVelocity;

    public Vector3 direction;

    // Damaged SFX
    public AudioClip blueRollSFX;

    // Counter
    private float counterToIdle = .25f;  //////// NEED TO MATCH

    //======================================
    void OnEnable()
    {
        Debug.Log("ROLL_State");

        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();
        bluePlayerController.canBeDamaged = false;

        // Disable Collider (INVULNERABILITY)
        //GetComponent<CapsuleCollider>().enabled = false;
        Physics.IgnoreLayerCollision(9, 21);
        Physics.IgnoreLayerCollision(9, 20);

        // Disable Firing
        GetComponent<BlueFiringPrimary>().enabled = false;

        // Move when Knocked Back
        myRigidbody = GetComponent<Rigidbody>();
        
        // Play DMG SFX
        AudioSource.PlayClipAtPoint(blueRollSFX, transform.position);
    }

    void Update()
    {
        // Direction and Force
        if (bluePlayerController.useController == true)
        {
            moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")) * /*24*/ 30;
        }

        if (bluePlayerController.useController == false)
        {
            moveVelocity = new Vector3(Input.GetAxisRaw("Key_X"), 0f, Input.GetAxisRaw("Key_Y")) * /*24*/ 30;
        }

        //Physics.IgnoreLayerCollision(9, 21);

        // Counter to Idle
        if (counterToIdle > 0)
        {
            counterToIdle -= Time.deltaTime;
        }

        if (counterToIdle <= 0)
        {
            counterToIdle = .25f; //////// NEED TO MATCH
            moveVelocity = new Vector3(0, 0, 0);
            this.enabled = false;
            // Enable Collieder (INVULNERABILITY)
            //GetComponent<CapsuleCollider>().enabled = true;
            Physics.IgnoreLayerCollision(9, 20, false);
            Physics.IgnoreLayerCollision(9, 21, false);
            bluePlayerController.canBeDamaged = true;
            GetComponent<Blue_Idle>().enabled = true;
        }
    }

    //======================================
    void FixedUpdate()  // Move the Rigid Body
    {
        myRigidbody.velocity = moveVelocity;
    }


}
