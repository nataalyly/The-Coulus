using UnityEngine;
using System.Collections;

public class HeartPickup : MonoBehaviour
{
    public float lifetime = 10f;
    public float blinkStart = 7f;
    private SpriteRenderer sr;
    private float timer = 0f;
    private bool isBlinking = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkStart && !isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.playerHP < 3)
            {
                GameManager.instance.playerHP++;
            }
            Destroy(gameObject);
        }
    }
}