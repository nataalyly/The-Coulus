using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private bool hasHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasHit)
        {
            hasHit = true;
            GameManager.instance.TakeDamage(1);
        }
    }
}
