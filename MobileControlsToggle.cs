using UnityEngine;

public class MobileControlsToggle : MonoBehaviour
{
    void Start()
    {
        bool isMobile = Input.touchSupported && SystemInfo.deviceType == DeviceType.Handheld;
        gameObject.SetActive(isMobile);
    }
}