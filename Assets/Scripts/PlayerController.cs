using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnMove.AddListener(MovePlayer);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnMove.RemoveListener(MovePlayer);
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
