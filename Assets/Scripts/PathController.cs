/**
* Credit: https://gist.github.com/codeimpossible/2704498b7b78240ccb08e5234b6a557c
*/

using UnityEngine;

public enum PathMovementStyle
{
    Continuous,
    Slerp,
    Lerp,
}
public class PathController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [Tooltip("A path object containing a series of points to move between.")]
    [SerializeField] private Transform PathContainer;
    [SerializeField] private PathMovementStyle MovementStyle;
    [SerializeField] private bool LoopThroughPoints;
    [SerializeField] private bool StartAtFirstPointOnAwake;

    private Transform[] _points;

    private int _currentTargetIdx;

    private void Awake()
    {
        _points = PathContainer.GetComponentsInChildren<Transform>();
        if (StartAtFirstPointOnAwake)
        {
            transform.position = _points[0].position;
        }
    }

    private void Update()
    {
        if (_points == null || _points.Length == 0) return;
        var distance = Vector3.Distance(transform.position, _points[_currentTargetIdx].position);
        if (distance * distance < 0.1f)
        {
            _currentTargetIdx++;
            if (_currentTargetIdx >= _points.Length)
            {
                _currentTargetIdx = LoopThroughPoints ? 0 : _points.Length - 1;
            }
        }
        switch (MovementStyle)
        {
            default:
            case PathMovementStyle.Continuous:
                transform.position = Vector3.MoveTowards(transform.position, _points[_currentTargetIdx].position, MovementSpeed * Time.deltaTime);
                break;
            case PathMovementStyle.Lerp:
                transform.position = Vector3.Lerp(transform.position, _points[_currentTargetIdx].position, MovementSpeed * Time.deltaTime);
                break;
            case PathMovementStyle.Slerp:
                transform.position = Vector3.Slerp(transform.position, _points[_currentTargetIdx].position, MovementSpeed * Time.deltaTime);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_points == null || _points.Length == 0) return;
        for (int i = 0; i < _points.Length; i++)
        {
            Gizmos.color = Color.yellow;
            if (i < _currentTargetIdx) Gizmos.color = Color.red;
            if (i > _currentTargetIdx) Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_points[i].position, 1f);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _points[_currentTargetIdx].position);
    }
}