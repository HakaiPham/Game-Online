using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flytrap : MonoBehaviour
{
    public Transform[] pathPoints; // Các điểm di chuyển (bao gồm vị trí gốc)
    public float moveSpeed = 3f;
    public float rotateSpeed = 360f; // độ/giây
    private int currentIndex = 0;

    void Update()
    {
        if (pathPoints.Length == 0) return;

        Transform target = pathPoints[currentIndex];
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        // Di chuyển
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Xoay theo hướng bay
        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime / 360f);
     //   transform.rotation = Quaternion.Euler(0, 0, angle);

        // Nếu đến điểm thì chuyển sang điểm tiếp theo
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex++;
            if (currentIndex >= pathPoints.Length)
            {
                currentIndex = 0; // quay lại vị trí ban đầu
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trap hit: " + collision.gameObject.name); // Debug message

        if (collision.CompareTag("Player")) // Use tag or name
        {
            Debug.Log("Player detected! Calling Die()");
            PlayerDeath player = collision.GetComponent<PlayerDeath>();
            if (player != null)
            {
                player.Die();
            }
            else
            {
                Debug.LogError("PlayerDeath script NOT found on Player!");
            }
        }
    }
}
