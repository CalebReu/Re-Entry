using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PathController[] pathControllers;
    private int currentMovementIdx = 0;
    void Start()
    {
        pathControllers = GetComponents<PathController>();
        pathControllers[currentMovementIdx].BeginMovement();
    }

    void Update()
    {
        if (pathControllers[currentMovementIdx].IsFinished())
        {
            if (currentMovementIdx >= pathControllers.Length - 1) return;
            currentMovementIdx++;
            pathControllers[currentMovementIdx].BeginMovement();
        }
    }
}
