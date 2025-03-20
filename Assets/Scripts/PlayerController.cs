using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using System.Collections;

public class PlayerController : SingletonMonoBehavior<PlayerController>
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private shotType equipped;
    enum shotType {SIMPLE, TRIPLE};

    private bool canFire = true;

    private Rigidbody2D rb;
    public GameObject simpleBullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        equipped = shotType.SIMPLE;
    }

    IEnumerator reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
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

        // Checks what is equipped
        switch (equipped)
        {
        case shotType.SIMPLE:
            Instantiate(simpleBullet, transform.position, transform.rotation);
            canFire = false;
            StartCoroutine(reload(0.5f));
            break;
        }
    }

    private void MovePlayer(Vector2 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }


}
