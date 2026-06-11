using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float blinkDuration = 0.2f;
    public int blinkCount = 3;

    public Sprite[] deathSprites;
    public float deathFrameDuration = 0.1f;
    
    public void TakeHit()
    {
        if (GameManager.instance.playerHP <= 0)
        {
            AudioManager.instance?.PlayHit();
            StartCoroutine(DeathAnimation());
        } else
        {
            AudioManager.instance?.PlayHit();
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSecondsRealtime(blinkDuration);
            sprite.enabled = true;
            yield return new WaitForSecondsRealtime(blinkDuration);
        }

        if (GameManager.instance.playerHP <= 0)
        {
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator DeathAnimation()
    {
        for (int i = 0; i < deathSprites.Length; i++)
        {
            sprite.sprite = deathSprites[i];
            yield return new WaitForSecondsRealtime(deathFrameDuration);
        }
        sprite.enabled = false;
    }
}