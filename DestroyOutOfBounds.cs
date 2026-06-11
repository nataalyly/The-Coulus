using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float minY = -6f;
    public float maxY = 7f;
    public float minX = -10f;
    public float maxX = 10f;

    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.y < minY || pos.y > maxY || pos.x < minX || pos.x > maxX)
        {
            Destroy(gameObject);
        }
    }
}
