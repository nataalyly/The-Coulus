using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    public AudioClip clickSound;
    public AudioClip pauseSound;
    public AudioClip gameOverSound;
    public AudioClip hitSound;
    public AudioClip bgmSound;
    public AudioClip bossAttackSound;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSound == null) return;
        musicSource.clip = bgmSound;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayClick()
    {
        if (clickSound == null) return;
            sfxSource.PlayOneShot(instance.clickSound);
    }
    public void PlayPaused()
    {
        if (pauseSound == null) return;
            sfxSource.PlayOneShot(instance.pauseSound);
    }

    public void PlayGameOver()
    {
        if (gameOverSound == null) return;
        sfxSource.PlayOneShot(gameOverSound);
    }

    public void PlayHit()
    {
        if (hitSound == null) return;
        sfxSource.PlayOneShot(hitSound);
    }

    public void PlayBossAttack()
    {
        if (bossAttackSound == null) return;
        sfxSource.PlayOneShot(bossAttackSound, 0.5f);
    }

    public void PlayThenLoadScene(AudioClip clip, string sceneName)
    {
        if (clip == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            return;
        }
        StartCoroutine(PlayAndLoadScene(clip, sceneName));
    }

    private IEnumerator PlayAndLoadScene(AudioClip clip, string sceneName)
    {
        sfxSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}