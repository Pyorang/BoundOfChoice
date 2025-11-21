using UnityEngine;

[RequireComponent(typeof(MonsterMovement))]
public class MonsterController : MonoBehaviour
{
    [Header("플레이어 추격 거리 설정")]
    [Tooltip("플레이어에게 접근을 멈추고 공격을 시작하는 최소 거리입니다.")]
    [SerializeField] private float _stopDistance;

    private MonsterMovement _movement;
    private Transform _target;
    private const string TargetTag = "Player";

    private void Awake()
    {
        _movement = GetComponent<MonsterMovement>();
    }

    private void Start()
    {
        // NOTE : 타겟을 찾아 Transform을 참조한다.
        GameObject player = GameObject.FindGameObjectWithTag(TargetTag);
        if (player != null)
        {
            _target = player.transform;
        }
    }

    private void Update()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (_target == null) return;

        float distance = _target.position.x - transform.position.x;

        float absDistance = Mathf.Abs(distance);

        Vector2 direction;

        if (absDistance < _stopDistance)
        {
            direction = Vector2.zero;
            // NOTE : 공격을 실행하는 로직을 추가한다.
        }
        else
        {
            float sign = Mathf.Sign(distance);
            direction = Vector2.right * sign;
        }

        _movement.SetMoveDirection(direction);
    }
}
