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

    private MonsterStats _stats;
    private MonsterMovement _movement;
    private MonsterNavigator _navigator;
    private MonsterAnimator _animator;
    private MonsterStatusEffect _statusEffect;
    private Transform _player;
    
    private Vector2 _moveDirection;
    private float _distanceToPlayer;

    private void Awake()
    {
        _stats = GetComponent<MonsterStats>();
        _movement = GetComponent<MonsterMovement>();
        _navigator = GetComponent<MonsterNavigator>();
        _animator = GetComponent<MonsterAnimator>();
        _statusEffect = GetComponent<MonsterStatusEffect>();
    }

    private void Start()
    {
        _player = PlayerMovement.Instance.transform;
    }

    private void OnEnable()
    {
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
        _distanceToPlayer = _player.position.x - transform.position.x;

        if (_state != EMonsterState.Patrol && _state != EMonsterState.Chase) return;
        DetermineState();
    }

    private void DetermineState()
    {
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
        ActionByState();
    }

    private void ActionByState()
    {
        switch (_state)
        {
            case EMonsterState.Chase:
                _moveDirection = _navigator.GetChaseDirection(_distanceToPlayer);
                HandleMove();
                break;
            case EMonsterState.Patrol:
                _moveDirection = _navigator.GetPatrolDirection();
                HandleMove();
                break;
            case EMonsterState.Attack:
                HandleAttack();
                break;
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

    private void HandleAttack()
    {
        _movement.StopMove();
        _animator.PlayAttackAnimation();
        _animator.SetSpriteFlip((_distanceToPlayer > 0) == _isSpriteLeft);
    }
    
    private void HandleMove()
    {
        _animator.PlayMoveAnimation(_moveDirection.x != 0);
        _animator.SetSpriteFlip((_moveDirection.x > 0) == _isSpriteLeft);
        _movement.SetMoveDirection(_moveDirection);
    }

    public void TakeDamage(int damage)
    {
        if (_state == EMonsterState.None) return;
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
        _movement.StopMove();

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
        DetermineState();
    }

    public void OnAnimationEnd()
    {
        DetermineState();
    }

    public void OnDeathAnimationEnd()
    {
        MonsterSpawner.Instance.CurrentMonsterCount--;
        Destroy(gameObject);
    }
}