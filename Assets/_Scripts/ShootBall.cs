using UnityEngine;

public class ShootBall : MonoBehaviour //ToDo ChangeColor and delete only the same color
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            Destroy(gameObject);
    }
}