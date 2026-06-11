using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefabs;
    public float spawnInterval = 1.5f;
    public float minSpeed = 1f;
    public float maxSpeed = 4f;
    public float minRotation = 20f;
    public float maxRotation = 100f;

    void Start()
    {
        InvokeRepeating("SpawnMeteor", 0f, spawnInterval);
    }

    void SpawnMeteor()
    {
        Debug.Log("SpawnMeteor called, prefab: " + meteorPrefabs);
        if (meteorPrefabs == null) return;

        Vector3 spawnPos = GetRandomEdgePosition();
        Vector2 direction = GetRandomDirection(spawnPos);

        GameObject meteor = Instantiate(meteorPrefabs, spawnPos, Quaternion.identity);

        float speed = Random.Range(minSpeed, maxSpeed);
        float rotation = Random.Range(minRotation, maxRotation);
        if (Random.value > 0.5f) rotation = -rotation;

        meteor.GetComponent<Meteor>()?.Init(direction, speed, rotation);
    }

    Vector3 GetRandomEdgePosition()
    {
        int edge = Random.Range(0, 4);
        return edge switch
        {
            0 => new Vector3(Random.Range(-10f, 10f), 8f, 0),
            1 => new Vector3(Random.Range(-10f, 10f), -8f, 0),
            2 => new Vector3(-12f, Random.Range(-6f, 6f), 0),
            _ => new Vector3(12f, Random.Range(-6f, 6f), 0),
        };
    }

    Vector2 GetRandomDirection(Vector3 spawnPos)
    {
        Vector3 target = new Vector3(
            Random.Range(-3f, 3f),
            Random.Range(-3f, 3f),
            0
        );
        return (target - spawnPos).normalized;
    }
}
