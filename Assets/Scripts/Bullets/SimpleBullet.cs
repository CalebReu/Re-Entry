using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 400f;
    [SerializeField] private int damage = 1;
    private Rigidbody2D rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        // Moves bullet forwards at assigned speed
        rb.linearVelocity = transform.up * speed * Time.deltaTime;
    }

    // Sets move speed of bullet
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
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
        transform.eulerAngles = transform.forward*degreeOffset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // TODO: Implement damage functionality once enemy functionality is added
            // Enemy enemy = collision.gameObject;
            // enemy.damage(damage);
        }
        // TODO: Explosion FX go here
        Destroy(gameObject);
    }
}
