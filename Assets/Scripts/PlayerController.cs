using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using System.Collections;

public class PlayerController : SingletonMonoBehavior<PlayerController>
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private shotType equipped;
    enum shotType { SIMPLE, TRIPLE, SHOTGUN };

    private bool canFire = true;
    private Rigidbody2D rb;
    public GameObject simpleBullet;

    // Increasable stats (1 means no change in stat):
    public float fireRateMod = 1f;
    public float bulletSpeedMod = 1f;
    public float bulletSizeMod = 0.3f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime / fireRateMod);
        canFire = true;
    }
    private void OnEnable()
    {
        InputManager.Instance.OnMove.AddListener(MovePlayer);
        InputManager.Instance.OnFire.AddListener(Fire);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnMove.RemoveListener(MovePlayer);
        InputManager.Instance.OnFire.RemoveListener(Fire);
    }

    private void Fire()
    {
        // Returns if can't fire
        if (!canFire) return;

        GameObject newBullet;
        SimpleBullet bulletScript;
        // Checks what is equipped
        switch (equipped)
        {

            case shotType.SIMPLE:
                newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                bulletScript = newBullet.GetComponent<SimpleBullet>();
                bulletScript.SetSpeed(bulletSpeedMod);
                bulletScript.SetSize(1 * bulletSizeMod);
                canFire = false;
                StartCoroutine(reload(0.5f));
                break;

            case shotType.TRIPLE:
                for (int i = 0; i < 3; i++)
                {
                    newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                    bulletScript = newBullet.GetComponent<SimpleBullet>();
                    float angle = -30 + (i * 30);
                    bulletScript.setAngle(angle);
                    bulletScript.SetSpeed(bulletSpeedMod);
                    bulletScript.SetSize(1 * bulletSizeMod);
                }
                canFire = false;
                StartCoroutine(reload(1.2f));
                break;
            case shotType.SHOTGUN:
                for (int i = 0; i < 5; i++)
                {
                    newBullet = Instantiate(simpleBullet, transform.position, transform.rotation);
                    bulletScript = newBullet.GetComponent<SimpleBullet>();
                    float angle = Random.Range(-80, 80);
                    bulletScript.setAngle(angle);
                    bulletScript.SetSpeed(bulletSpeedMod);
                    bulletScript.SetSize(0.3f * bulletSizeMod);
                }
                canFire = false;
                StartCoroutine(reload(1f));
                break;
        }
    }

    private void MovePlayer(Vector2 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }


}
