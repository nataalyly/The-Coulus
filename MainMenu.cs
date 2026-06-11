using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.instance?.PlayThenLoadScene(
        AudioManager.instance.clickSound, "GameScene");
    }
}
