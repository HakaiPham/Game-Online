using Fusion;
using UnityEngine;

public class CameraFollow2D : NetworkBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 0, -10); // Giữ nguyên Z cố định phía sau

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Camera.main.GetComponent<CameraFollow2D>().target = transform;
        }
    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = -10f; // luôn giữ camera ở z = -10

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
