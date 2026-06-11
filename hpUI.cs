using UnityEngine;

public class hpUI : MonoBehaviour
{
    public GameObject[] nyawa;
    void Update()
    {
        int hp = GameManager.instance.playerHP;
        for (int i = 0; i < nyawa.Length; i++)
        {
            nyawa[i].SetActive(i < hp);
        }
    }
}
