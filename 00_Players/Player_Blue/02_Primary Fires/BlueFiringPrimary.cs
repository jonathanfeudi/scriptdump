using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BlueFiringPrimary : MonoBehaviour
{
    // Player Controller
    private BluePlayerController bluePlayerController;

    // Health Controller
    private BluePlayerHealthManager blueHealthController;

    // Camera 
    private Camera mainCamera;

    // Animator
    Animator otherAnimator;

    //======================================

    void Awake()
    {
        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();

        // Sprite Animator
        otherAnimator = bluePlayerController.spriteObject.GetComponent<Animator>();
    }

    //======================================

    // Start is called before the first frame update
    void Start()
    {
        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();

        // Player Health Controller
        blueHealthController = GetComponent<BluePlayerHealthManager>();

        // Camera to Draw From
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        //======================================        KEYBOARD CONTROLLER

        // Fire and Rotate with Mouse
        if (!bluePlayerController.useController)
        {
            
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            if (bluePlayerController.canShoot == true)
            {

                // Fire Laser // - Primary 1
                if (Input.GetMouseButtonDown(0))
                    bluePlayerController.theGun.isFiringLaser = true;

                if (Input.GetMouseButtonUp(0))
                    bluePlayerController.theGun.isFiringLaser = false;

                // Laser - Alt Fire

                if (bluePlayerController.GetComponent<Blue_Warp>().enabled == false)
                {
                    if (Input.GetKeyDown("q"))
                        bluePlayerController.theGun.altFire = !bluePlayerController.theGun.altFire;
                }

                // Barrier
                if (Input.GetMouseButtonDown(1) & (bluePlayerController.theGun.canDeploy == true))
                {
                    bluePlayerController.theGun.canHoldBarrier = true;
                }
                    
                // Turret
                if (Input.GetMouseButtonDown(2) & (bluePlayerController.theGun.canDeploy == true))
                {
                    bluePlayerController.theGun.canHoldTurret = true;
                }
            }

            //======================================        

            // Animate Sprite
            if (Input.GetKey("w"))
            {
                otherAnimator.SetBool("isRunning", true);
                otherAnimator.SetFloat("Move_X", 0);
                otherAnimator.SetFloat("Move_Y", 1);
            }

            if (Input.GetKey("d"))
            {
                otherAnimator.SetBool("isRunning", true);
                otherAnimator.SetFloat("Move_X", 1);
                otherAnimator.SetFloat("Move_Y", 0);
            }

            if (Input.GetKey("s"))
            {
                otherAnimator.SetBool("isRunning", true);
                otherAnimator.SetFloat("Move_X", 0);
                otherAnimator.SetFloat("Move_Y", -1);
            }

            if (Input.GetKey("a"))
            {
                otherAnimator.SetBool("isRunning", true);
                otherAnimator.SetFloat("Move_X", -1);
                otherAnimator.SetFloat("Move_Y", 0);
            }

            if (!Input.GetKey("w") & !Input.GetKey("a") & !Input.GetKey("s") & !Input.GetKey("d"))
            {
                otherAnimator.SetBool("isRunning", false);
            }
        }

        //======================================        GAMEPAD CONTROLLER

        // Fire and Rotate with Controller
        if (bluePlayerController.useController)
        {
            // Movement //
            //Debug.Log("RH " + CrossPlatformInputManager.GetAxis("RHorizontal") + "RV " + CrossPlatformInputManager.GetAxis("RVertical"));

            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }

            //======================================

            // Animate Sprite
            if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
            {
                otherAnimator.SetBool("isRunning", true);
                otherAnimator.SetFloat("Move_X", Input.GetAxis("Horizontal"));
                otherAnimator.SetFloat("Move_Y", Input.GetAxis("Vertical"));
            }
            else
            {
                otherAnimator.SetBool("isRunning", false);
            }

            //======================================

            // Inputs //

            // Right Trigger - Laser FIRING

            if (bluePlayerController.canShoot == true)
            {
                if (Input.GetKey(KeyCode.Joystick1Button7))
                    bluePlayerController.theGun.isFiringLaser = true;

                // Right Bumper - Barrier

                if ((Input.GetKeyDown(KeyCode.Joystick1Button5)) & (bluePlayerController.theGun.canDeploy == true))
                {
                    bluePlayerController.theGun.canHoldBarrier = true; //!bluePlayerController.theGun.canHoldBarrier;//true;
                }

                // Left Bumper - Turret

                if ((Input.GetKeyDown(KeyCode.Joystick1Button4)) & (bluePlayerController.theGun.canDeploy == true))
                {
                    bluePlayerController.theGun.canHoldTurret = true; //!bluePlayerController.theGun.canHoldBarrier;//true;
                }

                // Y Button - Alt Fire  

                if (bluePlayerController.GetComponent<Blue_Warp>().enabled == false)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                        bluePlayerController.theGun.altFire = !bluePlayerController.theGun.altFire;
                }
            }

            // Right Trigger - Laser NOT FIRING

            if (!Input.GetKey(KeyCode.Joystick1Button7))
                bluePlayerController.theGun.isFiringLaser = false;

            //====================================== Formatted to Have Both GamePad and KeyBoard Controls Together - WILL NEED TO SEPARATE 

            // X Button - Heal

            if ((bluePlayerController.canShoot == true) & (bluePlayerController.blueHeals >= 1))
            {
                if ((Input.GetKeyDown(KeyCode.Keypad1)) || (Input.GetKeyDown(KeyCode.Joystick1Button2)))
                    {
                    bluePlayerController.blueHeals -= 1;
                    blueHealthController.currentHealth += 25;
                }
            }

        }
    }
}
