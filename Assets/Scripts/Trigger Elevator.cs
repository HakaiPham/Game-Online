using UnityEngine;
using Fusion;

public class TriggerElevator : NetworkBehaviour
{
    [SerializeField] private float limitUp;
    [SerializeField] private float limitDown;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int requiredPlayers = 2; // Minimum players to start moving up

    private int playersOnElevator = 0;
    private bool isMoving = false;
    private bool movingUp = false;

    private void Update()
    {
        if (!Object.HasStateAuthority) return;

        if (isMoving)
        {
            MoveElevator();
        }
    }

    private void MoveElevator()
    {
        if (movingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y >= limitUp)
            {
                transform.position = new Vector3(transform.position.x, limitUp, transform.position.z);
                isMoving = false;
            }
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y <= limitDown)
            {
                transform.position = new Vector3(transform.position.x, limitDown, transform.position.z);
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Object.HasStateAuthority) return;

        if (other.CompareTag("Player"))
        {
            playersOnElevator++;
            CheckStartElevator();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Object.HasStateAuthority) return;

        if (other.CompareTag("Player"))
        {
            playersOnElevator = Mathf.Max(playersOnElevator - 1, 0);
        }
    }

    private void CheckStartElevator()
    {
        if (playersOnElevator >= requiredPlayers && !isMoving)
        {
            movingUp = true;
            isMoving = true;
        }
    }

    public void StartMovingDown()
    {
        if (!Object.HasStateAuthority) return;

        movingUp = false;
        isMoving = true;
    }
}
