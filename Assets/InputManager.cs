using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{

    public UnityEvent<Vector2> OnMove = new();
    void Update()
    {

        Vector2 input = Vector2.zero;
        if (Input.GetKey()) {

        }

        if (Input.GetKey()) {

        }
    }
}
