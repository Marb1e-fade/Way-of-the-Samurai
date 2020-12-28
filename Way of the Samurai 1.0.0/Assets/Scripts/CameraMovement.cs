using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;

    private Vector3 velocity;

    private void FixedUpdate()
    {
        Vector3 finalPosition = target.position + offset;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);
        newPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        transform.position = newPosition;
    }
}