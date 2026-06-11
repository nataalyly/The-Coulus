using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerInput playerInput;
    public GameOverUI gameOverUI;
    public PlayerHealth playerHealth;
    public int score = 0;
    public int playerHP = 3;
    public int highscore;
    public bool isNewHighScore = false;
    public bool isPaused = false;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject mobileControls;
    
    [Header("Boss")]
    public GameObject bossPrefab;
    public int killsPerBoss = 20;
    private int killCount = 0;
    private bool bossAlive = false;
    private int bossKillCount = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }
    public void AddScore(int value, bool isKill = false)
    {
        score += value;
        
        if (score > highscore)
        {
            highscore = score;
        }

        if (isKill)
        {
            killCount++;
            if (killCount >= killsPerBoss && !bossAlive)
            {
                killCount = 0;
                SpawnBoss();
            }
        }
    }

    void SpawnBoss()
    {
        bossAlive = true;
        EnemySpawner.instance.StopSpawning();
        ScreenFlash.instance?.FlashRed(3);
        Vector3 spawnPos = new Vector3(0, 8f, 0);
        GameObject bossObj = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        int newHP = 100 + (bossKillCount * 50);
        bossObj.GetComponent<Boss>().hp = newHP;
    }

    public void TakeDamage(int damage)
    {
        playerHP -= damage;
        playerHealth.TakeHit();

        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator GameOverAfterAnimation()
    {
        float blinkTime = playerHealth.blinkDuration * playerHealth.blinkCount * 2;
        float deathTime = playerHealth.deathFrameDuration * playerHealth.deathSprites.Length;
        yield return new WaitForSecondsRealtime(blinkTime + deathTime);
        GameOver();
    }

    public void PauseGame()
    {
        isPaused = true;
        playerInput.DeactivateInput();
        AudioManager.instance?.PlayPaused();
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        playerInput.ActivateInput();
        AudioManager.instance?.PlayClick();
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.instance?.PlayThenLoadScene(
        AudioManager.instance.clickSound, "MainMenu");
    }

    public void OnBossDied()
    {
        bossKillCount++;
        bossAlive = false;
        EnemySpawner.instance.StartSpawning();
    }

    public void GameOver()
    {
        CameraShake.instance?.StopShake();
        isPaused = true;
        playerInput.DeactivateInput();
        AudioManager.instance?.PlayGameOver();
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        if (mobileControls != null) mobileControls.SetActive(false);
        int savedHigh = PlayerPrefs.GetInt("HighScore", 0);

        if (score > savedHigh)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            isNewHighScore = true;
        } else
        {
            isNewHighScore = false;
        }

        gameOverPanel.SetActive(true);
        gameOverUI.Setup();
    }
}
