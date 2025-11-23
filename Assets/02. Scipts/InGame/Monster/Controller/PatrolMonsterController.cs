using UnityEngine;

public class PatrolMonsterController : MonsterController
{
    [Header("순찰 설정")]
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

    protected override void Init()
    {
        CancelInvoke(nameof(NextDirection));
        NextDirection();
    }

    protected override Vector2 GetMoveDirection()
    {
        Vector2 viewPos = _mainCamera.WorldToViewportPoint(transform.position);

        if (viewPos.x < 0 || viewPos.x > 1)
        {
            _patrolDirection = -_patrolDirection;
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