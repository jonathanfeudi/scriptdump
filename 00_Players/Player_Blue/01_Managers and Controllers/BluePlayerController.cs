using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;

public class BluePlayerController : MonoBehaviour 
{
    [Header("Initialized Variables")]
    
    // Current State
    public List<string> statusList;

    // Movement
    //public float moveSpeed;
    private Rigidbody myRigidbody;

    public Vector3 moveInput;
    public Vector3 moveVelocity;
    public float moveSpeed;

    // Can Shoot / Can Act
    public bool canShoot = true;

    // Can Be Damaged
    public bool canBeDamaged;

    // Camera 
    private Camera mainCamera;

    // Gun
    public BlueGunController theGun;

    // Warp Points
    public GameObject deploy_1_ID;
    public GameObject deploy_2_ID;
    public Vector3 deploy_1;
    public Vector3 deploy_2;

    // Heals
    public int blueHeals = 3;

    // Associated Sprite Object
    public GameObject spriteObject;

    // Sprite Animator
    Animator Animator;

    // GamePad or Mouse
    public bool useController;

    public Gamepad myGamepad;

    //======================================

    void Awake()
    {
        Animator = spriteObject.GetComponent<Animator>();

        // Can Be Damaged
        canBeDamaged = true;

        // Add "idle" state to status list
        statusList.Add("Blue_Idle");
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        // Enter Idle State
        //GetComponent<Blue_Idle>().enabled = true;
    }

    void Update()
    {
        foreach(string state in statusList)
        {
            Debug.Log(state + "BPC");
            MonoBehaviour script = (MonoBehaviour)gameObject.GetComponent(state);
            script.enabled = true;
        }

        //myGamepad = Gamepad.current;
        //moveInput = new Vector3(myGamepad.leftStick.x.ReadValue(), 0f, myGamepad.leftStick.y.ReadValue());

        
        //moveVelocity = moveInput * moveSpeed;

        var currentGamepad = Gamepad.current;
        var leftAnalogX = currentGamepad.leftStick.x.ReadValue();
        var leftAnalogY = currentGamepad.leftStick.y.ReadValue();
        if ((leftAnalogY != 0 || leftAnalogX !=0) && !(checkForState("Blue_Running", statusList)))
        {
            statusList.Add("Blue_Running");
            var idleState = statusList.IndexOf("Blue_Idle");
            statusList.RemoveAt(idleState);
        }

        /*
        Debug.Log("LS X: " + currentGamepad.leftStick.x.ReadValue() + "LS Y: " + currentGamepad.leftStick.y.ReadValue());
        Debug.Log("RS X: " + currentGamepad.rightStick.x.ReadValue() + "RS Y: " + currentGamepad.rightStick.y.ReadValue());
        Debug.Log(currentGamepad.buttonSouth.isPressed);
        */
    }

    private static bool checkForState(string status, List<string> myList)
    {
        foreach(string state in myList)
        {
            if(state == status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

/*
    void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }
    */
}

//======================================

/*

A
joystick button 0

B
joystick button 1

X
joystick button 2

Y
joystick button 3

Left Bumper
joystick button 4

Right Bumper
joystick button 5


View(Back)
joystick button 6

Menu(Start)
joystick button 7

Left Stick Button
joystick button 8

Right Stick Button
joystick button 9

Left Stick “Horizontal”
X Axis
-1 to 1

Left Stick “Vertical”
Y Axis
1 to -1

Right Stick “HorizontalTurn”
4th Axis
-1 to 1

Right Stick “VerticalTurn”
5th Axis
1 to -1

DPAD – Horizontal
6th Axis
-1 (.64) 1

DPAD – Vertical
7th Axis
-1 (.64) 1

Left Trigger
9th Axis
0 to 1

Right Trigger
10th Axis
0 to 1

Left Trigger Shared Axis
3rd Axis
0 to 1

Right Trigger Shared Axis
3rd Axis
0 to -1

*/ // GAME PAD LEGEND