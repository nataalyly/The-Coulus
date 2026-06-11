using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public static BossHPBar instance;
    public Slider slider;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Show(int maxHP)
    {
        gameObject.SetActive(true);
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void UpdateHP(int currentHP)
    {
        slider.value = currentHP;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
