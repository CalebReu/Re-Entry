using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private int damage = 1;
    private float speed;
    private Rigidbody2D rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider>();
        speed = baseSpeed;
        Debug.Log("Bullet created");
    }

    void Update()
    {
        // Moves bullet forwards at assigned speed
        rb.linearVelocity = transform.up * speed;
    }

    // Sets move speed of bullet
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed * baseSpeed;
    }

    // Sets damage of bullet
    public void SetDamage(int dmg)
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
            Destroy(gameObject);
        }
        // TODO: Explosion FX go here

    }
}
