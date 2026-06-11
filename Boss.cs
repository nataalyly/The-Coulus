using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public int hp = 100;
    public int scoreReward = 150;

    [Header("Movement")]
    public float entrySpeed = 2f;
    public float targetY = 3.5f;
    public float floatAmplitude = 0.3f;
    public float floatSpeed = 1.5f;

    [Header("Attack")]
    public Transform firePoint;
    public float attackInterval = 1.5f;
    public float warningDuration = 0.2f;
    public float laserDuration = 1f;
    public float laserWidth = 0.1f;

    [Header("Laser Visual")]
    public GameObject warningLinePrefab;
    public GameObject laserSpritePrefab;

    private bool isInPosition = false;
    private float floatBaseY;
    private float floatTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;

        BossHPBar.instance?.Show(hp);
    }

    void Update()
    {
        if (!isInPosition)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(0, targetY, 0),
                entrySpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, new Vector3(0, targetY, 0)) < 0.1f)
            {
                isInPosition = true;
                floatBaseY = transform.position.y;
                StartCoroutine(AttackLoop());
            }
        } else {
            floatTimer += Time.deltaTime * floatSpeed;
            float newY = floatBaseY + Mathf.Sin(floatTimer) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            yield return StartCoroutine(FireLaser());
        }
    }

    IEnumerator FireLaser()
    {
        if (playerTransform == null) yield break;

        float distance = 20f;

        Vector3 targetPos = playerTransform.position;
        Vector3 startPos = firePoint != null ? firePoint.position : transform.position;
        Vector3 direction = (targetPos - startPos).normalized;
        Vector3 endPos = startPos + direction * distance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Vector3 midPos = (startPos + endPos) / 2f;

        GameObject warning = Instantiate(warningLinePrefab, midPos, Quaternion.Euler(0, 0, angle));
        warning.transform.localScale = new Vector3(10f, distance, 1f);
        SpriteRenderer warningSR = warning.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            warningSR.enabled = true;
            yield return new WaitForSeconds(0.15f);
            warningSR.enabled = false;
            yield return new WaitForSeconds(0.15f);
        }
        warningSR.enabled = true;

        yield return new WaitForSeconds(warningDuration);
        Destroy(warning);

        AudioManager.instance?.PlayBossAttack();

        GameObject laser = Instantiate(laserSpritePrefab, midPos, Quaternion.Euler(0, 0, angle));
        laser.transform.localScale = new Vector3(15f, distance, 1f);
        CameraShake.instance?.Shake(0.3f, 0.2f);

        RaycastHit2D hit = Physics2D.Raycast(
            startPos, direction, distance, LayerMask.GetMask("Player")
        );
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(1);
        }

        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);
    }

    public void TakeHit()
    {
        hp--;
        BossHPBar.instance?.UpdateHP(hp);

        StartCoroutine(HitFlash());
        if (hp <= 0)
        {
            Die();
        }
    }

    IEnumerator HitFlash()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.grey;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        BossHPBar.instance?.Hide();
        
        foreach (var laser in GameObject.FindGameObjectsWithTag("LaserBeam"))
        {
            Destroy(laser);
        }
        foreach (var warning in GameObject.FindGameObjectsWithTag("WarningLine"))
        {
            Destroy(warning);
        }

        GameManager.instance.AddScore(scoreReward);
        GameManager.instance.OnBossDied();
        Destroy(gameObject);
    }
}
