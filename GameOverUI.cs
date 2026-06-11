using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject newHighScoreText;
    public void Setup()
    {
        finalScoreText.text = "Score: " + GameManager.instance.score;
        
        if (GameManager.instance.highscore == 0)
        {
            highScoreText.text = "High Score: -";
        } else
        {
            highScoreText.text = "High Score: " + GameManager.instance.highscore;
        }

        newHighScoreText.SetActive(GameManager.instance.isNewHighScore);
    }
}
