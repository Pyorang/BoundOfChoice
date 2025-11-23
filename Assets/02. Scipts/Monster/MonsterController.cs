using UnityEngine;

[RequireComponent(typeof(MonsterMovement))]
public abstract class MonsterController : MonoBehaviour
{
    [Header("플레이어 추격 거리 설정")]
    [Tooltip("플레이어에게 접근을 멈추고 공격을 시작하는 최소 거리입니다.")]
    [SerializeField] protected float _stopDistance;

    protected MonsterMovement _movement;
    protected MonsterAnimator _animator;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected GameObject _player;
    
    private Vector2 _direction;
    protected float _distance;

    private void Awake()
    {
        _movement = GetComponent<MonsterMovement>();
        _animator = GetComponent<MonsterAnimator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init() { }

    private void Update()
    {
        if (_player == null) return;
        DetermineState();
        HandleMove();
    }

    private void DetermineState()
    {
        _distance = _player.transform.position.x - transform.position.x;

        if (IsPlayerInAttackRange())
        {
            HandleAttack();
        }
        else
        {
            HandleMoveDirection();
        }
    }

    private bool IsPlayerInAttackRange()
    {
        return Mathf.Abs(_distance) < _stopDistance;
    } 

    private void SetSpriteFlip(bool flip)
    {
        _spriteRenderer.flipX = flip;
    }

    private void HandleAttack()
    {
        _direction = Vector2.zero;
        // NOTE : 스킬 시전 로직을 추가한다.
        _animator.PlayAttackAnimation();
        SetSpriteFlip(_distance > 0);
    }

    private void HandleMoveDirection()
    {
        _direction = GetMoveDirection();
        SetSpriteFlip(_direction.x > 0);
    }

    private void HandleMove()
    {
        bool isMoving = _direction != Vector2.zero;

        _animator.PlayMoveAnimation(isMoving);
        _movement.SetMoveDirection(_direction);
    }

    protected abstract Vector2 GetMoveDirection();

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}