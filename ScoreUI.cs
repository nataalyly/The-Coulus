using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameManager.instance != null)
        {
            scoreText.text = "Score: " + GameManager.instance.score;
        }
    }
}
