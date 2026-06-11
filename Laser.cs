using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeHit();
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>()?.TakeHit();
            Destroy(gameObject);
        }

    }
}
