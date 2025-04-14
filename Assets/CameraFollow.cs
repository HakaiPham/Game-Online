using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;              // The player to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Default offset
    public float smoothSpeed = 5f;        // Follow speed

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        transform.LookAt(target); // Optional: make camera always look at the player
    }
}
