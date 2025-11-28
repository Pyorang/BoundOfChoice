using System.Collections;
using UnityEngine;

public enum EMonsterState
{
    Move,
    Attack,
    Freeze,
    Hurt,
    Death,
    Spawning,
    Patrol,
    Chase,
}

[RequireComponent(typeof(MonsterMovement))]
public abstract class MonsterController : MonoBehaviour
{
    [Header("플레이어 추격 거리 설정")]
    [Tooltip("플레이어에게 접근을 멈추고 공격을 시작하는 최소 거리입니다.")]
    [SerializeField] protected float _stopDistance;

    [Header("시작 상태")]
    [Space]
    [SerializeField] private EMonsterState _state;
    [SerializeField] private bool _isLeft;

    [Header("타격감 설정")]
    [Space]
    [SerializeField] private float _hitShakeDuration = 0.25f;
    [SerializeField] private float _hitShakeIntensity = 0.1f;
    [SerializeField] private float _deathShakeDuration = 0.25f;
    [SerializeField] private float _deathShakeIntensity = 0.75f;

    protected MonsterMovement _movement;
    protected MonsterAnimator _animator;
    private MonsterStats _stats;
    private MonsterStatusEffect _statusEffect;
    protected SpriteRenderer _spriteRenderer;
    protected Transform _player;
    
    private Vector2 _direction;
    protected float _distance;

    private Coroutine _bindCoroutine;
    private Coroutine _dotDamageCoroutine;


    private void Awake()
    {
        _movement = GetComponent<MonsterMovement>();
        _animator = GetComponent<MonsterAnimator>();
        _stats = GetComponent<MonsterStats>();
        _statusEffect = GetComponent<MonsterStatusEffect>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = PlayerMovement.Instance.transform;
    }

    private void OnEnable()
    {
        Init();
        _statusEffect.OnDotDamageTick += HandleDotDamage;
        _statusEffect.OnBindStart += HandleBindStart;
        _statusEffect.OnBindEnd += HandleBindEnd;
    }

    private void OnDisable()
    {
        _statusEffect.OnDotDamageTick -= HandleDotDamage;
        _statusEffect.OnBindStart -= HandleBindStart;
        _statusEffect.OnBindEnd -= HandleBindEnd;
    }

    protected virtual void Init() { }

    private void Update()
    {
        if (_player == null) return;
        DetermineState();
    }

    private void DetermineState()
    {
        _distance = _player.position.x - transform.position.x;
        switch (_state)
        {
            case EMonsterState.Move:
                HandleMoveDirection();
                HandleMove();
                break;
            case EMonsterState.Attack:
                HandleAttack();
                break;
            case EMonsterState.Spawning:
                return;
        }
    }

    private bool IsPlayerInAttackRange()
    {
        return Mathf.Abs(_distance) < _stopDistance;
    } 

    private void SetSpriteFlip(bool flip)
    {
        _spriteRenderer.flipX = flip == _isLeft;
    }

    private void HandleAttack()
    {
        SetSpriteFlip(_distance > 0);
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
        _direction = GetMoveDirection();
        SetSpriteFlip(_direction.x > 0);
    }

    private void HandleMove()
    {
        if (IsPlayerInAttackRange())
        {
            _state = EMonsterState.Attack;
            _direction = Vector2.zero;
            _animator.PlayMoveAnimation(false);
            _animator.PlayAttackAnimation();
        }
        _movement.SetMoveDirection(_direction);
    }

    protected abstract Vector2 GetMoveDirection();

    public void TakeDamage(int damage)
    {
        _stats.TakeDamage(damage);
        _direction = Vector2.zero;
        _movement.SetMoveDirection(_direction);
        _movement.ApplyKnockback(_player);
        
        if (_stats.CurrentHealth <= 0)
        {
            Death();
            CameraController.Instance.StartShake(_deathShakeDuration, _deathShakeIntensity);
            return;
        }

        CameraController.Instance.StartShake(_hitShakeDuration, _hitShakeIntensity);
        _state = EMonsterState.Hurt;
        _animator.PlayHitAnimation();
    }

    public void Death()
    {
        _state = EMonsterState.Death;
        _animator.PlayDeathAnimation();
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
        // NOTE : 추후 Take Damage 로직을 stats로 분리하며 이 곳을 추가한다.
        // NOTE : 카메라 쉐이크가 적용되지 않는 TakeDamage 로직이다.
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
        _state = EMonsterState.Move;
    }

    public void OnAnimationEnd()
    {
        _state = EMonsterState.Move;
        _animator.PlayMoveAnimation(true);
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(this.gameObject);
    }
}