using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerController : SingletonMonoBehavior<PlayerController>
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private shotType equipped;
    enum shotType {SIMPLE, TRIPLE};

    private Rigidbody2D rb;
    public GameObject simpleBullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        equipped = shotType.SIMPLE;
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
        Debug.Log("Fire!");
        // Checks what is equipped
        switch (equipped)
        {
        case shotType.SIMPLE:
            Instantiate(simpleBullet, transform.position, transform.rotation);
            break;
        }
    }

    private void MovePlayer(Vector2 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }


}
