using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    
    void Update()
    {
        int highscore = GameManager.instance.highscore;

        if (highscore == 0)
        {
            highScoreText.text = "High Score: -";
        }
        else
        {
            highScoreText.text = "High Score: " + highscore;
        }
    }
}
