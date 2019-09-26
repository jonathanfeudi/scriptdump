using UnityEngine;

using System.Collections;

public class DetectControlMethod : MonoBehaviour
{

    public BluePlayerController bluePlayerController;
    // Start is called before the first frame update
    void Start()
    {
        // Player Controller
        bluePlayerController = GetComponent<BluePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect Mouse Input
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            bluePlayerController.useController = false;

        if (Input.GetAxisRaw("Mouse X") != 0.0f || (Input.GetAxisRaw("Mouse Y") != 0.0f))
            bluePlayerController.useController = false;

        // D-Pad
        if (Input.GetAxis("DPad_X") != 0.0f || (Input.GetAxis("DPad_Y") != 0.0f))
            bluePlayerController.useController = true;

        // Detect Controller Input
        if (Input.GetAxisRaw("RHorizontal") != 0.0f || (Input.GetAxisRaw("RVertical") != 0.0f))
            bluePlayerController.useController = true;

        if (Input.GetKey(KeyCode.Joystick1Button0) |    // A
            Input.GetKey(KeyCode.Joystick1Button1) ||   // B
            Input.GetKey(KeyCode.Joystick1Button2) ||   // X
            Input.GetKey(KeyCode.Joystick1Button3) ||   // Y
            Input.GetKey(KeyCode.Joystick1Button4) ||   // L_Bumper
            Input.GetKey(KeyCode.Joystick1Button5) ||   // R Bumper
            Input.GetKey(KeyCode.Joystick1Button6) ||   // Back
            Input.GetKey(KeyCode.Joystick1Button7) ||   // Start
            Input.GetKey(KeyCode.Joystick1Button8) ||   // L_Stick_Button
            Input.GetKey(KeyCode.Joystick1Button9) ||   // R_Stick_Button
            
            // Triggers
            Input.GetAxis("RTrigger") > 0 ||
            Input.GetAxis("LTrigger") > 0 ||

            // Left Stick
            Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0 
            )
            bluePlayerController.useController = true;
    }
}
