using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(1);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
