using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    private PathController[] pathControllers;
    private Transform bulletSpawnPoint;
    private int currentMovementIdx = 0;
    private float fireTimer = 0f;
    void Start()
    {
        bulletSpawnPoint = transform.Find("BulletSpawnRef");
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
        simpleBullet.setAngle(180);
        simpleBullet.SetSpeed(bulletSpeed);
        simpleBullet.SetSize(0.3f);

    }
}
