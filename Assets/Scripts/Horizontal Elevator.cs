using UnityEngine;

public class HorizontalElevator : MonoBehaviour
{
    [SerializeField] private float limitLeft = -5f;
    [SerializeField] private float limitRight = 5f;
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        // Calculate the new position using Mathf.PingPong
        float newX = Mathf.PingPong(Time.time * speed, limitRight - limitLeft) + limitLeft;

        // Update the position
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
    }

    private void OnValidate()
    {
        // Ensure limits are valid
        if (limitLeft > limitRight)
        {
            Debug.LogWarning("LimitLeft should be less than LimitRight. Adjusting values.");
            float temp = limitLeft;
            limitLeft = limitRight;
            limitRight = temp;
        }
    }
}
