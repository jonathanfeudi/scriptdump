using UnityEngine;

public class SpriteFollowTarget : MonoBehaviour
{
    public Transform target;  // Object to follow
    public Vector3 offset;

    private void LateUpdate() //LateUpdate may produce jitters...
    {
        Vector3 desiredPosition = target.position + offset; // Gets position we want to snap to

        transform.position = desiredPosition;
    }
}
