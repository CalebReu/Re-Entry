/**
* Credit: https://gist.github.com/codeimpossible/2704498b7b78240ccb08e5234b6a557c
*/

using UnityEngine;

public enum PathMovementStyle
{
    Continuous,
    Slerp, // may remove if unused
    Lerp,  // may remove if unused
}
public class PathController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [Tooltip("A path object containing a series of points to move between.")]
    [SerializeField] private Transform PathContainer;
    [SerializeField] private PathMovementStyle MovementStyle;
    [SerializeField] private bool LoopThroughPoints;
    [SerializeField] private bool StartAtFirstPointOnAwake;
    [SerializeField] private float pauseAtEnd = 0;
    [SerializeField] private bool randomizePause = false;
    [SerializeField] private int teleportAtIndex = -1;
    [SerializeField] public float tiltAmount = 6;
    private float currentTiltAngle = 0f;
    public float direction;
    public float tiltSmoothness = 5f;
    private Transform[] _points;

    private int _currentTargetIdx;
    private float _pauseTimer;
    private bool _isMoving;
    private bool _isFinished = false;

    private void Awake()
    {
        _points = PathContainer.GetComponentsInChildren<Transform>();
        _pauseTimer = randomizePause ? Random.Range(0, pauseAtEnd) : pauseAtEnd;
        if (StartAtFirstPointOnAwake)
        {
            transform.position = _points[0].position;
        }
    }

    public void BeginMovement()
    {
        _isMoving = true;
    }
    public bool IsFinished()
    {
        return _isFinished;
    }

    private void Update()
    {
        if (_points == null || _points.Length == 0 || !_isMoving) return;
        var distance = Vector3.Distance(transform.position, _points[_currentTargetIdx].position);
        direction = transform.position.x - _points[_currentTargetIdx].position.x;
        if (distance * distance < 0.1f)
        {
            ResetTilt();
            _currentTargetIdx++;
            if (_currentTargetIdx >= _points.Length)
            {
                _currentTargetIdx = _points.Length - 1; // stay at last point
                if (_pauseTimer > 0) _pauseTimer -= Time.deltaTime;
                else if (LoopThroughPoints)
                {
                    _pauseTimer = randomizePause ? Random.Range(0, pauseAtEnd) : pauseAtEnd; // reset pause timer
                    _currentTargetIdx = 0;
                }
                else
                {
                    _isMoving = false;
                    _isFinished = true;
                }
            }
            else if (teleportAtIndex == _currentTargetIdx - 1)
            {
                transform.position = _points[_currentTargetIdx].position;
            }
        }
        else {
            TiltSprite(direction);
         
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
    private void TiltSprite(float direction)
    {
        direction = direction > 0 ? 1 : -1;
       
        float targetAngle = direction * tiltAmount;
        currentTiltAngle = Mathf.LerpAngle(currentTiltAngle, targetAngle, Time.deltaTime * tiltSmoothness);
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, currentTiltAngle);
    }
    private void ResetTilt()
    {
        currentTiltAngle = Mathf.LerpAngle(currentTiltAngle, Quaternion.identity.z, Time.deltaTime * tiltSmoothness);
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, currentTiltAngle);
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
        if (_currentTargetIdx < _points.Length)
            Gizmos.DrawLine(transform.position, _points[_currentTargetIdx].position);
    }
}