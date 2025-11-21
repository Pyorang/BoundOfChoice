using UnityEngine;

[RequireComponent(typeof(MonsterMovement))]
public abstract class MonsterController : MonoBehaviour
{
    [Header("플레이어 추격 거리 설정")]
    [Tooltip("플레이어에게 접근을 멈추고 공격을 시작하는 최소 거리입니다.")]
    [SerializeField] protected float _stopDistance;

    protected MonsterMovement _movement;
    protected GameObject _player;

    private void Awake()
    {
        _movement = GetComponent<MonsterMovement>();
    }

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init() { }

    private void Update()
    {
        if (_player == null) return;

        float distance = _player.transform.position.x - transform.position.x;
        float absDistance = Mathf.Abs(distance);

        Vector2 direction;
        if (absDistance < _stopDistance)
        {
            direction = Vector2.zero;
            // NOTE : 스킬 시전 로직을 추가한다.
        }
        else
        {
            direction = GetMoveDirection();
        }

        _movement.SetMoveDirection(direction);
    }

    protected abstract Vector2 GetMoveDirection();

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}