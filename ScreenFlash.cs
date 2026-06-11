using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash instance;
    public Image flashImage;

    void Awake()
    {
        instance = this;
    }

    public void FlashRed(int count = 3)
    {
        StartCoroutine(DoFlash(count));
    }

    IEnumerator DoFlash(int count)
    {
        for (int i = 0; i < count; i++)
        {
            flashImage.color = new Color(1, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.2f);
            flashImage.color = new Color(1, 0, 0, 0f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
