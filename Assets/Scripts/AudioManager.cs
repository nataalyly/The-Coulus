using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource musicSource2;
    public AudioClip clickSound;
    public AudioClip pauseSound;
    public AudioClip gameOverSound;
    public AudioClip hitSound;
    public AudioClip bgmSound;
    public AudioClip bossAttackSound;
    public AudioClip bossMusicClip;

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

    public void CrossFadeToBossMusic()
    {
        StartCoroutine(CrossFade(musicSource, musicSource2, bossMusicClip));
    }

    public void CrossFadeToNormalMusic()
    {
        StartCoroutine(CrossFade(musicSource2, musicSource, bgmSound));
    }

    IEnumerator CrossFade(AudioSource fadeOut, AudioSource fadeIn, AudioClip newClip)
    {
        float duration = 1.5f;
        float elapsed = 0f;

        fadeIn.clip = newClip;
        fadeIn.loop = true;
        fadeIn.volume = 0f;
        fadeIn.Play();

        float startVolume = fadeOut.volume;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            fadeOut.volume = Mathf.Lerp(startVolume, 0f, t);
            fadeIn.volume = Mathf.Lerp(0f, startVolume, t);
            yield return null;
        }

        fadeOut.Stop();
        fadeOut.volume = startVolume;
    }
}