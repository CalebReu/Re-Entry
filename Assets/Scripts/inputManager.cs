using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : SingletonMonoBehavior<InputManager>
{

  public UnityEvent<Vector2> OnMove;
  public UnityEvent OnFire;
  void FixedUpdate()
  {
    Vector3 moveVector = Vector3.zero;
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveVector += Vector3.left;
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveVector += Vector3.right;
    if (Input.GetKey(KeyCode.Space)) OnFire?.Invoke(); // fire button
    OnMove?.Invoke(moveVector);
  }
}
