using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Camera Follows this object
    public float smoothSpeed = 0.25f; // Amount of Time to Reach Target

    public Vector3 offset;

    // Shake //

    Vector3 cameraInitialPosition;
    public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
    public Camera mainCamera;

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.Range(-2.0f, 2.0f) * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = Random.Range(-2.0f, 2.0f) * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }

    private void FixedUpdate() //LateUpdate may produce jitters...
    {
        Vector3 desiredPosition = target.position + offset; // Gets position we want to snap to
        // Smoothing
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 2 * Time.deltaTime);  // Lerps to get a bit closer to that position. smooth speed dictates how quickly it lerps.
        // transform.position = desiredPosition; // No Smoothing

        transform.position = smoothedPosition; 
    }
}
