using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.instance?.PlayThenLoadScene(
        AudioManager.instance.clickSound, "MainMenu");
    }
    public void Restart()
    {
        AudioManager.instance?.PlayClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
}
