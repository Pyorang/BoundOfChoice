using UnityEngine;

public class MonsterNavigator : MonoBehaviour
{
    [Header("순찰 설정")]
    [Space]
    [SerializeField] private float _minInterval;
    [SerializeField] private float _maxInterval;

    private const int LeftVector = -1;
    private const int RightVector = 2;
    private Vector2 _patrolDirection;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        NextDirection();
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(NextDirection));
    }

    public Vector2 GetChaseDirection(float distance)
    {
        float sign = Mathf.Sign(distance);
        return Vector2.right * sign;
    }

    public Vector2 GetPatrolDirection()
    {
        Vector2 viewPos = _mainCamera.WorldToViewportPoint(transform.position);

        if(viewPos.x < 0)
        {
            _patrolDirection = Vector2.right;
        }
        else if(viewPos.x > 1)
        {
            _patrolDirection = Vector2.left;
        }

        return _patrolDirection;
    }

    private void NextDirection()
    {
        _patrolDirection = Vector2.right * Random.Range(LeftVector, RightVector);

        float interval = Random.Range(_minInterval, _maxInterval);
        Invoke(nameof(NextDirection), interval);
    }

}
