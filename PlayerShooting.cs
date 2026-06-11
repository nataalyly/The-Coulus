using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource audioSource;
    public AudioClip shootSound;

    private bool isPointerOverUI = false;

    void Update()
    {
        isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void onShoot(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;
        if (isPointerOverUI) return;
        if (context.performed)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void MobileShoot()
    {
        if (GameManager.instance.isPaused) return;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(shootSound);
    }
}
