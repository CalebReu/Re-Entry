using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeedModifier;
    [SerializeField] private int startingHealth;
    private float health;
    private PathController[] pathControllers;
    private Transform bulletSpawnPoint;
    private int currentMovementIdx = 0;
    private float fireTimer = 0f;
    void Start()
    {
        bulletSpawnPoint = transform.Find("BulletSpawnRef");
        health = startingHealth;
        fireTimer = UnityEngine.Random.Range(0, 1 / fireRate); // first shot is random
        pathControllers = GetComponents<PathController>();
        pathControllers[currentMovementIdx].BeginMovement();
    }

    void Update()
    {
        if (fireTimer <= 0)
        {
            Fire();
            fireTimer = 1 / fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
        if (pathControllers[currentMovementIdx].IsFinished())
        {
            if (currentMovementIdx >= pathControllers.Length - 1) return;
            currentMovementIdx++;
            pathControllers[currentMovementIdx].BeginMovement();
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        SimpleBullet simpleBullet = bullet.GetComponent<SimpleBullet>();
        simpleBullet.SetEnemy();
        simpleBullet.setAngle(180);
        simpleBullet.SetSpeed(bulletSpeedModifier);
        simpleBullet.SetSize(0.3f);

    }
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // enemy dead whomp whomp
            GameManager.Instance.UpdateEnemyCount();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player with enemy body");
            // TODO: Implement player damage
        }
    }
}
