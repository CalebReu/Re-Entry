using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float damage = 1;
    private float speed;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    [SerializeField] private float topBound, bottomBound; // to check when bullet is off screen

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        speed = baseSpeed;

        if (Camera.main != null)
        {
            topBound = Camera.main.orthographicSize;
            bottomBound = -topBound;
        }
    }

    void Update()
    {
        // Moves bullet forwards at assigned speed
        rb.linearVelocity = transform.up * speed;

        if (isOffScreen())
        {
            Destroy(gameObject);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Hit enemy");
            GameManager.Instance.UpdateScore();
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Damage((int)damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet")
        {
            Debug.Log("Hit player");
            // TODO: Implement player damage
            Destroy(gameObject);
        }
        // TODO: Explosion FX go here

    }

    // helper fn to check if bullet is off screen, so we know when we can delete it
    // in order to not clog up resources
    private bool isOffScreen()
    {
        Vector3 pos = transform.position;
        float bulletHeight = col.radius; // already divided in 1/2 since radius

        return (pos.y >= topBound + bulletHeight) ||
        (pos.y <= bottomBound - bulletHeight);
    }
}
