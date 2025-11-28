using UnityEngine;

public enum EMonsterState
{
    Patrol,
    Chase,
    Attack,
    Freeze,
    Hurt,
    Death,
}

[RequireComponent(typeof(MonsterMovement), typeof(MonsterAnimator), typeof(MonsterStats))]
[RequireComponent(typeof(MonsterStatusEffect))]
public class MonsterController : MonoBehaviour
{
    [Header("플레이어 추격 거리 설정")]
    [Space]
    [Tooltip("플레이어를 발견하고 추격을 시작하는 최소 거리입니다.")]
    [SerializeField] private float _chaseDistance;
    [Tooltip("플레이어에게 접근을 멈추고 공격을 시작하는 최소 거리입니다.")]
    [SerializeField] private float _attackDistance;

    [Header("순찰 설정")]
    [Space]
    [SerializeField] private float _minInterval;
    [SerializeField] private float _maxInterval;

    [Header("시작 상태")]
    [Space]
    [SerializeField] private EMonsterState _state;
    [SerializeField] private bool _isSpriteLeft;

    [Header("타격감 설정")]
    [Space]
    [SerializeField] private float _hitShakeDuration = 0.25f;
    [SerializeField] private float _hitShakeIntensity = 0.1f;
    [SerializeField] private float _deathShakeDuration = 0.25f;
    [SerializeField] private float _deathShakeIntensity = 0.75f;

    private MonsterMovement _movement;
    private MonsterAnimator _animator;
    private MonsterStats _stats;
    private MonsterStatusEffect _statusEffect;
    private SpriteRenderer _spriteRenderer;
    private Transform _player;
    
    private Vector2 _moveDirection;
    private float _distanceToPlayer;

    private const int LeftVector = -1;
    private const int RightVector = 2;
    private Vector2 _patrolDirection;
    private Camera _mainCamera;

    private void Awake()
    {
        _movement = GetComponent<MonsterMovement>();
        _animator = GetComponent<MonsterAnimator>();
        _stats = GetComponent<MonsterStats>();
        _statusEffect = GetComponent<MonsterStatusEffect>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _player = PlayerMovement.Instance.transform;
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        CancelInvoke(nameof(NextDirection));
        NextDirection(); 
        
        _stats.OnDeath += HandleDeath;
        _stats.OnDamageTaken += HandleTakeDamage;
        _statusEffect.OnDotDamageTick += HandleDotDamage;
        _statusEffect.OnBindStart += HandleBindStart;
        _statusEffect.OnBindEnd += HandleBindEnd;
    }

    private void OnDisable()
    {
        _stats.OnDeath -= HandleDeath;
        _stats.OnDamageTaken -= HandleTakeDamage;
        _statusEffect.OnDotDamageTick -= HandleDotDamage;
        _statusEffect.OnBindStart -= HandleBindStart;
        _statusEffect.OnBindEnd -= HandleBindEnd;
    }

    private void Update()
    {
        if (_player == null) return;
        DetermineState();
    }

    private void DetermineState()
    {
        _distanceToPlayer = _player.position.x - transform.position.x;
        switch (_state)
        {
            case EMonsterState.Chase:
                HandleMoveDirection();
                HandleMove();
                break;
            case EMonsterState.Patrol:
                break;
            case EMonsterState.Attack:
                HandleAttack();
                break;
        }
    }

    private bool IsPlayerInAttackRange()
    {
        return Mathf.Abs(_distanceToPlayer) < _attackDistance;
    } 

    private void SetSpriteFlip(bool flip)
    {
        _spriteRenderer.flipX = flip == _isSpriteLeft;
    }

    private void HandleAttack()
    {
        SetSpriteFlip(_distanceToPlayer > 0);
    }
    
    public void DealDamage()
    {
        if (IsPlayerInAttackRange())
        {
            PlayerHealth.Instance.TakeDamage(_stats.AttackPower);
        }
    }

    private void HandleMoveDirection()
    {
        _moveDirection = GetChaseDirection();
        SetSpriteFlip(_moveDirection.x > 0);
    }

    private void HandleMove()
    {
        if (IsPlayerInAttackRange())
        {
            _state = EMonsterState.Attack;
            _moveDirection = Vector2.zero;
            _animator.PlayMoveAnimation(false);
            _animator.PlayAttackAnimation();
        }
        _movement.SetMoveDirection(_moveDirection);
    }

    private Vector2 GetChaseDirection()
    {
        float sign = Mathf.Sign(_distanceToPlayer);
        return Vector2.right * sign;
    }

    private Vector2 GetPatrolDirection()
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

    public void TakeDamage(int damage)
    {
        _stats.TakeDamage(damage);
    }

    private void HandleTakeDamage()
    {
        _movement.StopMove();
        _movement.ApplyKnockback(_player);

        CameraController.Instance.StartShake(_hitShakeDuration, _hitShakeIntensity);
        _state = EMonsterState.Hurt;
        _animator.PlayHitAnimation();
    }

    public void HandleDeath()
    {
        _state = EMonsterState.Death;
        _animator.PlayDeathAnimation();
        CameraController.Instance.StartShake(_deathShakeDuration, _deathShakeIntensity);
    }

    public void TakeDotDamage(int damage, float duration, float interval)
    {
        if (_stats.CurrentHealth <= 0) return;
        _statusEffect.ApplyDotDamage(damage, duration, interval);
    }

    public void TakeBind(float duration)
    {
        _statusEffect.ApplyBind(duration);
    }

    private void HandleDotDamage(int damage)
    {
        _movement.StopMove();

        _state = EMonsterState.Hurt;
        _animator.PlayHitAnimation();
    }

    private void HandleBindStart()
    {
        _state = EMonsterState.Freeze;
        _stats.SetMoveSpeed(0);
        _animator.StopAnimation();
    }

    private void HandleBindEnd()
    {
        _animator.ResumeAnimation();
        _stats.ResetSpeed();
        _state = EMonsterState.Chase;
    }

    public void OnAnimationEnd()
    {
        _state = EMonsterState.Chase;
        _animator.PlayMoveAnimation(true);
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(this.gameObject);
    }
}