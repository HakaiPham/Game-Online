using UnityEngine;

public class TelePoint : MonoBehaviour
{
    [Tooltip("Kéo thả GameObject TelePoint vào đây")]
    public Transform telePoint; // Điểm dịch chuyển đến

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu vật thể chạm vào có tag "Player"
        if (collision.CompareTag("Player"))
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Phiên bản cho 3D
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        if (telePoint != null)
        {
            // Dịch chuyển player đến vị trí telePoint
            player.transform.position = telePoint.position;

            // Reset velocity nếu có Rigidbody
            Rigidbody2D rb2D = player.GetComponent<Rigidbody2D>();
            Rigidbody rb3D = player.GetComponent<Rigidbody>();

            if (rb2D != null) rb2D.linearVelocity = Vector2.zero;
            if (rb3D != null) rb3D.linearVelocity = Vector3.zero;

            Debug.Log("Player đã được dịch chuyển đến checkpoint");
        }
        else
        {
            Debug.LogWarning("Chưa gán TelePoint trong Inspector");
        }
    }
}