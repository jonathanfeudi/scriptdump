using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Warp : MonoBehaviour
{
    // Player Controller
    public BluePlayerController bluePlayerController;

    // Blue Gun
    public BlueGunController blueGunController;

    // Warp Target
    public float targetSwitch = 0;

    public bool triggerDown = false;

    public bool dpadPressed = false;

    // Alt Warp - Retrieve Target
    public bool altWarp = false;

    // Movement
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    //======================================

    void OnEnable()
    {
        Debug.Log("WARP_State");

        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();

        bluePlayerController.canShoot = false;  // Disable Firing Script

        // Gun Controller
        //blueGunController = GetComponent<BlueGunController>();  // NOT NEEDED AS IT IS DRAGGED INTO THE INSPECTOR

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
        //======================================

        // WARP to TARGET
        var mouseScrollWheelDelta = Input.GetAxis("MouseScrollWheel");

        // Change Selection
        if (blueGunController.barrierCounter > 1)
        {
            if ((mouseScrollWheelDelta > 0f) || ((Input.GetAxis("DPad_X") > 0) & (dpadPressed == false)))
            {
                targetSwitch += 1;
                dpadPressed = true;
            }

            if ((mouseScrollWheelDelta < 0f) || ((Input.GetAxis("DPad_X") < 0) & (dpadPressed == false)))
            {
                targetSwitch -= 1;
                dpadPressed = true;
            }

            if (Input.GetAxis("DPad_X") == 0)
            {
                dpadPressed = false;
            }
        }

        // Loop Selection
        if (targetSwitch > 1)
        {
            targetSwitch = 0;
        }

        if (targetSwitch < 0)
        {
            targetSwitch = 1;
        }

        // Trigger Down Variable

        if (/*Input.GetAxis("LTrigger") <= 0*/ !Input.GetKey(KeyCode.Joystick1Button6))
        {
            triggerDown = true;
        }

        // Warp to Selection
        if (altWarp == false)
        {
            if ((targetSwitch == 0) & ((Input.GetKeyDown("e")) || (/*(Input.GetAxis("LTrigger") > 0*/ Input.GetKeyDown(KeyCode.Joystick1Button6) & triggerDown == true)))
            {
                transform.position = bluePlayerController.deploy_1;
                Destroy(bluePlayerController.deploy_1_ID);
                if (bluePlayerController.deploy_2_ID != null)
                {
                    //bluePlayerController.deploy_1 = bluePlayerController.deploy_2;
                    bluePlayerController.deploy_1_ID = bluePlayerController.deploy_2_ID;
                    bluePlayerController.deploy_2_ID = null;
                    //bluePlayerController.deploy_2 = Vector3.zero;
                }
                else
                {
                    bluePlayerController.deploy_1_ID = null;
                    //bluePlayerController.deploy_1 = Vector3.zero;
                }
                bluePlayerController.theGun.barrierCounter -= 1;
                bluePlayerController.theGun.canDeploy = true;
                this.enabled = false;
                GetComponent<Blue_Idle>().enabled = true;
            }

            if ((targetSwitch == 1) & ((Input.GetKeyDown("e")) || (/*(Input.GetAxis("LTrigger") > 0*/ Input.GetKeyDown(KeyCode.Joystick1Button6) & triggerDown == true)))
            {
                transform.position = bluePlayerController.deploy_2;
                bluePlayerController.theGun.barrierCounter -= 1;
                bluePlayerController.theGun.canDeploy = true;
                bluePlayerController.deploy_2 = Vector3.zero;
                Destroy(bluePlayerController.deploy_2_ID);
                bluePlayerController.deploy_2_ID = null;
                targetSwitch = 0;
                this.enabled = false;
                GetComponent<Blue_Idle>().enabled = true;
            }
        }

        // Retrieve Deployables
        if (altWarp == true)
        {
            if ((targetSwitch == 0) & ((Input.GetKeyDown("e")) || (/*(Input.GetAxis("LTrigger") > 0*/ Input.GetKeyDown(KeyCode.Joystick1Button6) & triggerDown == true)))
            {
                Destroy(bluePlayerController.deploy_1_ID);
                if (bluePlayerController.deploy_2_ID != null)
                {
                    bluePlayerController.deploy_1 = bluePlayerController.deploy_2;
                    bluePlayerController.deploy_1_ID = bluePlayerController.deploy_2_ID;
                    bluePlayerController.deploy_2_ID = null;
                    bluePlayerController.deploy_2 = Vector3.zero;
                }
                else
                {
                    bluePlayerController.deploy_1_ID = null;
                    bluePlayerController.deploy_1 = Vector3.zero;
                }
                bluePlayerController.theGun.barrierCounter -= 1;
                bluePlayerController.theGun.canDeploy = true;
                this.enabled = false;
                GetComponent<Blue_Idle>().enabled = true;
            }

            if ((targetSwitch == 1) & ((Input.GetKeyDown("e")) || (/*(Input.GetAxis("LTrigger") > 0*/ Input.GetKeyDown(KeyCode.Joystick1Button6) & triggerDown == true)))
            {
                bluePlayerController.theGun.barrierCounter -= 1;
                bluePlayerController.theGun.canDeploy = true;
                bluePlayerController.deploy_2 = Vector3.zero;
                Destroy(bluePlayerController.deploy_2_ID);
                bluePlayerController.deploy_2_ID = null;
                targetSwitch = 0;
                this.enabled = false;
                GetComponent<Blue_Idle>().enabled = true;
            }
        }


        // Highlight Selected Warp Point
        if ((targetSwitch == 0) & (bluePlayerController.deploy_1_ID != null))
        {
            if (bluePlayerController.deploy_1_ID.name == "Blue_Turret(Clone)")
            {
                bluePlayerController.deploy_1_ID.GetComponent<BlueTurretController>()._selected = true;

                if (bluePlayerController.deploy_2_ID != null)
                {
                    if (bluePlayerController.deploy_2_ID.name == "Blue_Turret(Clone)")
                    {
                        bluePlayerController.deploy_2_ID.GetComponent<BlueTurretController>()._selected = false;
                    }
                    if (bluePlayerController.deploy_2_ID.name == "Blue_Barrier(Clone)")
                    {
                        bluePlayerController.deploy_2_ID.GetComponent<BlueBarrierController>()._selected = false;
                    }
                }
                
            }

            if (bluePlayerController.deploy_1_ID.name == "Blue_Barrier(Clone)")
            {
                bluePlayerController.deploy_1_ID.GetComponent<BlueBarrierController>()._selected = true;

                if (bluePlayerController.deploy_2_ID != null)
                {
                    if (bluePlayerController.deploy_2_ID.name == "Blue_Turret(Clone)")
                    {
                        bluePlayerController.deploy_2_ID.GetComponent<BlueTurretController>()._selected = false;
                    }
                    if (bluePlayerController.deploy_2_ID.name == "Blue_Barrier(Clone)")
                    {
                        bluePlayerController.deploy_2_ID.GetComponent<BlueBarrierController>()._selected = false;
                    }
                }
            }
        }

        if ((targetSwitch == 1) & (bluePlayerController.deploy_2_ID != null))
        {
            if (bluePlayerController.deploy_2_ID.name == "Blue_Turret(Clone)")
            {
                bluePlayerController.deploy_2_ID.GetComponent<BlueTurretController>()._selected = true;

                if (bluePlayerController.deploy_1_ID.name == "Blue_Turret(Clone)")
                {
                    bluePlayerController.deploy_1_ID.GetComponent<BlueTurretController>()._selected = false;
                }
                if (bluePlayerController.deploy_1_ID.name == "Blue_Barrier(Clone)")
                {
                    bluePlayerController.deploy_1_ID.GetComponent<BlueBarrierController>()._selected = false;
                }
            }

            if (bluePlayerController.deploy_2_ID.name == "Blue_Barrier(Clone)")
            {
                bluePlayerController.deploy_2_ID.GetComponent<BlueBarrierController>()._selected = true;

                if (bluePlayerController.deploy_1_ID.name == "Blue_Turret(Clone)")
                {
                    bluePlayerController.deploy_1_ID.GetComponent<BlueTurretController>()._selected = false;
                }
                if (bluePlayerController.deploy_1_ID.name == "Blue_Barrier(Clone)")
                {
                    bluePlayerController.deploy_1_ID.GetComponent<BlueBarrierController>()._selected = false;
                }
            }

        }

        // Toggle Alt Warp
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown("q"))
        {
            altWarp = !altWarp;
        }

        // Cancel Warp

        if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            triggerDown = false;
            this.enabled = false;
            GetComponent<Blue_Idle>().enabled = true;
        }
    }

    // Turn Off Alt Warp Variable
    void OnDisable()
    {
        bluePlayerController.canShoot = true;  // Enable Firing Script
        altWarp = false;
    }
}