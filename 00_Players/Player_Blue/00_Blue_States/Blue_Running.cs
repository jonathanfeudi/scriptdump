using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Blue_Running : MonoBehaviour
{
    // Player Controller
    private BluePlayerController bluePlayerController;

    // Movement
    public float moveSpeed;
    private Rigidbody myRigidbody;

    public Vector3 moveInput;
    public Vector3 moveVelocity;

    public Gamepad myGamepad;



    //======================================

    void OnEnable()
    {
        //Debug.Log("Running_State");

        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();

        //Move Player
        myRigidbody = GetComponent<Rigidbody>();

        // Enable Firing
        GetComponent<BlueFiringPrimary>().enabled = true;
    }

    //======================================

    void Update() // RUNNING TO IDLE, Movement, Firing, and Aiming // Update is called once per frame
    {
        //======================================

        // RUNNING to IDLE
        /*
        if (bluePlayerController.useController)
        {
            if ((Input.GetAxisRaw("Horizontal") == 0) & (Input.GetAxisRaw("Vertical") == 0))
            {
                // If player uses movement inputs, run:
                this.enabled = false;
                GetComponent<Blue_Idle>().enabled = true;
            }
        } else
        {
            if ((Input.GetAxisRaw("Key_X") == 0) & (Input.GetAxisRaw("Key_Y") == 0))
            {
                // If player uses movement inputs, run:
                this.enabled = false;
                GetComponent<Blue_Idle>().enabled = true;
            }
        }

        // RUNNING to ROLL
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.enabled = false;
            GetComponent<Blue_Roll>().enabled = true;
        }

        // IDLE to WARP
        if ((Input.GetKey(KeyCode.Joystick1Button6) && bluePlayerController.deploy_1 != Vector3.zero) || (Input.GetKeyDown("e") && bluePlayerController.deploy_1 != Vector3.zero))
        {
            if (bluePlayerController.canShoot == true)
            {
                if (GetComponent<Blue_Warp>().triggerDown == false)
                {
                    this.enabled = false;
                    GetComponent<Blue_Warp>().enabled = true;
                }
            }
        }

        // Reset Warp Trigger
        if (!Input.GetKey(KeyCode.Joystick1Button6))
        {
            GetComponent<Blue_Warp>().triggerDown = false;
        }

        GetComponent<Blue_Roll>().direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));


        // RUNNING to DAMAGED
        if (GetComponent<Blue_Damaged>().enabled == true)
        {
            this.enabled = false;
        }
        */

        //======================================

        // Movement
        //Debug.Log("Running!!!");


/*
        if (bluePlayerController.useController)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        }
        else
        {
            moveInput = new Vector3(Input.GetAxisRaw("Key_X"), 0f, Input.GetAxisRaw("Key_Y"));
        }
*/
        var currentGamepad = Gamepad.current;
        //Debug.Log("LS X: " + currentGamepad.leftStick.x.ReadValue() + "LS Y: " + currentGamepad.leftStick.y.ReadValue());
        moveInput = new Vector3(currentGamepad.leftStick.x.ReadValue(), 0f, currentGamepad.leftStick.y.ReadValue());
        moveVelocity = moveInput * moveSpeed;
        Debug.Log("Blue running");
    }
        

    //======================================

    void FixedUpdate()  // Move the Rigid Body
    {
        myRigidbody.velocity = moveVelocity;
    }
}
