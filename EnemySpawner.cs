using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public GameObject enemyPrefab;
    private bool isStopped = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 2f);
    }
    void SpawnEnemy()
    {
        if (isStopped) return;
        float x = Random.Range(-8f, 8f);
        Vector3 pos = new Vector3(x, 6f, 0);

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    public void StopSpawning() => isStopped = true;
    public void StartSpawning() => isStopped = false;
}
