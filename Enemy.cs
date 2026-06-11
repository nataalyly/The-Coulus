using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    public GameObject bulletPrefab;
    public float shootInterval = 2f;
    public float raycastDistance = 15f;
    private float shootTimer = 0f;
    private Transform playerTransform;
    public int hp = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * speed;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            CheckAndShoot();
        }
    }

    void CheckAndShoot()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            directionToPlayer,
            raycastDistance,
            LayerMask.GetMask("Player")
        );

        Debug.DrawRay(transform.position, directionToPlayer * raycastDistance, Color.red, 0.5f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Shoot(directionToPlayer);
        }
    }

    void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = direction * 5f;

        Physics2D.IgnoreCollision(
            bullet.GetComponent<Collider2D>(),
            GetComponent<Collider2D>()
        );
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void TakeHit()
    {
        hp--;
        if (hp <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10, true);
            }
            Destroy(gameObject);
        }
    }
}
