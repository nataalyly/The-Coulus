using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Sprite[] meteorSprites;
    public float speed;
    public float rotationSpeed;
    private Vector2 direction;

    void Awake()
    {
        if (meteorSprites.Length > 0)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = meteorSprites[Random.Range(0, meteorSprites.Length)];
            Debug.Log("Sprite assigned: " + sr.sprite);

            float scale = Random.Range(0.5f, 2f);
            transform.localScale = new Vector3(scale, scale, 1f);
        } else
        {
            Debug.Log("meteorSprites kosong!");
        }
    }

    public void Init(Vector2 dir, float spd, float rotSpd)
    {
        direction = dir;
        speed = spd;
        rotationSpeed = rotSpd;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        if (pos.x < -15f || pos.x > 15f || pos.y < -10f || pos.y > 10f)
        {
            Destroy(gameObject);
        }
    }
}
