using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float lifeSpan = 10;
    [SerializeField] private float damage = 1f;
    private float speed;
    private Rigidbody2D rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider>();
        speed = baseSpeed;
    }

    void Update()
    {
        // Moves bullet forwards at assigned speed
        rb.linearVelocity = transform.up * speed;
        updateLifeSpan();//kills the bullet after 10 seconds.
    }

    // Sets move speed of bullet
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed * baseSpeed;
    }

    // Sets damage of bullet
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetSize(float sizeChange)
    {
        transform.localScale = new Vector3(sizeChange, sizeChange, 1);
    }
    public void setAngle(float degreeOffset)
    {
        transform.eulerAngles = transform.forward * degreeOffset;
    }

    public void SetEnemy()
    {
        gameObject.tag = "EnemyBullet";
    }

    private void updateLifeSpan() {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Hit enemy");
            GameManager.Instance.UpdateScore();
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Damage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet")
        {
            Debug.Log("Hit player");
            GameManager.Instance.loseLive(damage);
            Destroy(gameObject);
        }
        // TODO: Explosion FX go here

    }
}
