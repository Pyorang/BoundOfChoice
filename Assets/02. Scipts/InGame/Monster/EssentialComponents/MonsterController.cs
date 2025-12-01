using UnityEngine;

[RequireComponent(typeof(MonsterMovement), typeof(MonsterAnimator), typeof(MonsterStats))]
[RequireComponent(typeof(MonsterStatusEffect), typeof(MonsterState))]
public class MonsterController : MonoBehaviour
{
    [Header("몬스터 설정")]
    [Space]
    [SerializeField] private EPoolType _type;
    [SerializeField] private bool _isSpriteLeft;

    [Header("타격감 설정")]
    [Space]
    [SerializeField] private float _hitShakeDuration = 0.25f;
    [SerializeField] private float _hitShakeIntensity = 0.1f;
    [SerializeField] private float _deathShakeDuration = 0.25f;
    [SerializeField] private float _deathShakeIntensity = 0.75f;

    private MonsterStats _stats;
    private MonsterState _state;
    private MonsterMovement _movement;
    private MonsterNavigator _navigator;
    private MonsterAnimator _animator;
    private MonsterStatusEffect _statusEffect;
    private ItemDropper _itemDropper;
    
    private Vector2 _moveDirection;

    private void Awake()
    {
        _stats = GetComponent<MonsterStats>();
        _state = GetComponent<MonsterState>();
        _movement = GetComponent<MonsterMovement>();
        _navigator = GetComponent<MonsterNavigator>();
        _animator = GetComponent<MonsterAnimator>();
        _statusEffect = GetComponent<MonsterStatusEffect>();
        _itemDropper = GetComponent<ItemDropper>();
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
        if (_state.State != EMonsterState.Patrol && _state.State != EMonsterState.Chase) return;
        _state.DetermineState();
        ActionByState();
    }

    private void ActionByState()
    {
        switch (_state.State)
        {
            case EMonsterState.Chase:
                _moveDirection = _navigator.GetChaseDirection(_state.DistanceToPlayer);
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

    private void HandleAttack()
    {
        _movement.StopMove();
        _animator.PlayAttackAnimation();
        _animator.SetSpriteFlip((_state.DistanceToPlayer > 0) == _isSpriteLeft);
    }
    
    private void HandleMove()
    {
        _animator.PlayMoveAnimation(_moveDirection.x != 0);
        _animator.SetSpriteFlip((_moveDirection.x > 0) == _isSpriteLeft);
        _movement.SetMoveDirection(_moveDirection);
    }
    
    private void HandleDash()
    {
        _moveDirection = transform.right;
        _movement.SetMoveDirection(_moveDirection);
    }

    private void HandleSmash()
    {
        _moveDirection = _navigator.GetChaseDirection(_state.GetDistanceToPlayer());
        _animator.SetSpriteFlip((_moveDirection.x > 0) == _isSpriteLeft);
    }

    private void HandleCast()
    {
        _movement.StopMove();
        _state.SetState(EMonsterState.Attack);
    }

    public void TakeDamage(int damage)
    {
        if (_state.State == EMonsterState.None) return;
        _stats.TakeDamage(damage);
    }

    private void HandleTakeDamage()
    {
        _movement.StopMove();
        _movement.ApplyKnockback(_state.DistanceToPlayer);

        CameraController.Instance.StartShake(_hitShakeDuration, _hitShakeIntensity);
        _state.SetState(EMonsterState.Hurt);
        _animator.PlayHitAnimation();
    }

    public void HandleDeath()
    {
        _movement.StopMove();

        _state.SetState(EMonsterState.Death);
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

        _state.SetState(EMonsterState.Hurt);
        _animator.PlayHitAnimation();
    }

    private void HandleBindStart()
    {
        _state.SetState(EMonsterState.Freeze);
        _stats.SetMoveSpeed(0);
        _animator.StopAnimation();
    }

    private void HandleBindEnd()
    {
        _animator.ResumeAnimation();
        _stats.ResetSpeed();
        _state.DetermineState();
    }

    private void OnAnimationEnd()
    {
        _state.DetermineState();
        ActionByState();
    }

    private void OnDeathAnimationEnd()
    {
        MonsterSpawner.Instance.CurrentMonsterCount--;
        PoolManager.Instance.ReleaseObject(_type, gameObject);
        if (_itemDropper == null) return;
        _itemDropper.TryDropItem();
    }
}
