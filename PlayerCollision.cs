using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            GameManager.instance.TakeDamage(1);
        }
    }
}