using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLaze : MonoBehaviour
{
    [System.Serializable]
    public class ArrowShot
    {
        public Transform firePoint;     // Vị trí xuất phát
        public Vector2 direction = Vector2.down; // Hướng bắn
    }

    public GameObject arrowPrefab;
    public float arrowSpeed = 5f;
    public float fireRate = 1f; // bắn mỗi giây
    public ArrowShot[] shots; // danh sách các điểm bắn

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            FireAllArrows();
            fireTimer = 0f;
        }
    }

    void FireAllArrows()
    {
        foreach (ArrowShot shot in shots)
        {
            if (shot.firePoint != null)
            {
                GameObject arrow = Instantiate(arrowPrefab, shot.firePoint.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shot.direction.normalized * arrowSpeed;
            }
        }
    }
}
