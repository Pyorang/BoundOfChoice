using UnityEngine;

public enum EMonsterState
{
    None,
    Patrol,
    Chase,
    Attack,
    Freeze,
    Hurt,
    Death,
}

public class MonsterState : MonoBehaviour
{
    [Header("시작 상태")]
    [Space]
    [SerializeField] private EMonsterState _state;
    private EMonsterState _defaultState;

    [Header("플레이어 추격 거리 설정")]
    [Space]
    [Tooltip("플레이어를 발견하고 추격을 시작하는 최소 거리입니다.")]
    [SerializeField] private float _chaseDistance;
    [Tooltip("플레이어에게 접근을 멈추고 공격을 시작하는 최소 거리입니다.")]
    [SerializeField] private float _attackDistance;

    private Transform _player;
    private float _distanceToPlayer;
    public float DistanceToPlayer => _distanceToPlayer;

    private void Awake()
    {
        _defaultState = _state;
    }

    private void Start()
    {
        _player = PlayerMovement.Instance.transform;
    }

    private void OnEnable()
    {
        _state = _defaultState;
    }

    public void DetermineState()
    {
        if (_player == null) return;
        _distanceToPlayer = _player.position.x - transform.position.x;

        if (IsPlayerInAttackRange())
        {
            _state = EMonsterState.Attack;
        }
        else if (IsPlayerInSight())
        {
            _state = EMonsterState.Chase;
        }
        else
        {
            _state = EMonsterState.Patrol;
        }
    }

    private bool IsPlayerInSight()
    {
        return Mathf.Abs(_distanceToPlayer) < _chaseDistance;
    }

    private bool IsPlayerInAttackRange()
    {
        return Mathf.Abs(_distanceToPlayer) < _attackDistance;
    }

    public float GetDistanceToPlayer()
    {
        return _player.position.x - transform.position.x;
    }

    public void SetState(EMonsterState state)
    {
        _state = state;
    }

    public EMonsterState State => _state;
}
