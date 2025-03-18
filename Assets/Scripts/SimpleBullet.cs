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
        rb.linearVelocity = Vector2.up * speed * Time.deltaTime;
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

    // TODO: Uncomment once other bullet types are being implemented. No use for single shots
    // public void setAngle(float degreeOffset)
    // {
    //     float upAngle = Vector2.Angle(Vector2.up);
    //     float newAngle = upAngle + degreeOffset;
    // }

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
