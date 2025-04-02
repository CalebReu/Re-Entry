using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeedModifier;
    [SerializeField] private int startingHealth;
    [SerializeField] private float minShootDistance;
    [SerializeField] private Animator anim;

    private float health;
    private PathController[] pathControllers;
    private Transform bulletSpawnPoint;
    private int currentMovementIdx = 0;
    private float fireTimer = 0f;
    bool dead = false;
    bool hurt = false;
    void Start()
    {
        anim = GetComponent<Animator>();
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
        anim.SetBool("hurt", hurt);
    }

    public void Fire()
    {
        if (transform.position.y <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + minShootDistance) return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        SimpleBullet simpleBullet = bullet.GetComponent<SimpleBullet>();
        simpleBullet.SetEnemy();
        simpleBullet.setAngle(180);
        simpleBullet.SetSpeed(bulletSpeedModifier);
        simpleBullet.SetSize(0.3f);
        AudioManager.instance.PlaySound(AudioManager.instance.enemyShootClip);

    }
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            dead = true;
            // enemy dead whomp whomp
            GameManager.Instance.UpdateEnemyCount();
            AudioManager.instance.PlaySound(AudioManager.instance.explosionClip);
            Destroy(gameObject);
        }
        checkDamage();
    }
    private void checkDamage() {
        if (health <= startingHealth / 2 && !hurt && startingHealth>1) {
            hurt = !hurt;
            anim.enabled = true;
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player with enemy body");
              GameManager.Instance.loseLive(1); // this is hardcoded for now (maybe forever).
           
            AudioManager.instance.PlaySound(AudioManager.instance.hitClip);
            // TODO: Implement player damage
        }
    }
}
