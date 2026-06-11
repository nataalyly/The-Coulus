using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject heartPrefab;

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 15f, 15f);
    }

    void SpawnPowerUp()
    {
        if (GameManager.instance.playerHP >= 3) return;

        float x = Random.Range(-7f, 7f);
        float y = Random.Range(-3f, 3f);
        Instantiate(heartPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
