using System.Collections;
using UnityEngine;

public enum EMonsterState
{
    Move,
    Attack,
    Freeze,
    Hurt,
    Death,
}

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
    private MonsterStats _stats;

    private EMonsterState _state;

    private Coroutine _bindCoroutine;
    private Coroutine _dotDamageCoroutine;


    private void Awake()
    {
        _movement = GetComponent<MonsterMovement>();
        _animator = GetComponent<MonsterAnimator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _stats = GetComponent<MonsterStats>();
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
    }

    private void DetermineState()
    {
        _distance = _player.transform.position.x - transform.position.x;
        switch (_state)
        {
            case EMonsterState.Move:
                HandleMoveDirection();
                HandleMove();
                break;
            case EMonsterState.Attack:
                HandleAttack();
                break;
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

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    public void TakeDamage(int damage)
    {
        _stats.TakeDamage(damage);
        _direction = Vector2.zero;
        _movement.SetMoveDirection(_direction);
        Vector2 knockBackDirection = -(Vector2.right * _distance);
        _movement.ApplyKnockback(knockBackDirection);
        if (_stats.CurrentHealth <= 0)
        {
            Death();
            CameraController.Instance.StartShake(0.25f, 0.75f);
            return;
        }
        else
        {
            CameraController.Instance.StartShake(0.25f, 0.1f);
        }
         _state = EMonsterState.Hurt;
        _animator.PlayHurtAnimation();
    }

    public void Death()
    {
        _state = EMonsterState.Death;
        _animator.PlayDieAnimation();
    }

    public void TakeBind(float duration)
    {
        if (_bindCoroutine != null)
        {
            StopCoroutine(_bindCoroutine);
        }
        _bindCoroutine = StartCoroutine(ProcessBind(duration));
    }

    private IEnumerator ProcessBind(float duration)
    {
        _state = EMonsterState.Freeze;
        _stats.SetMoveSpeed(0);
        _animator.StopAnimation();

        yield return new WaitForSeconds(duration);

        _animator.ResumeAnimation();
        _stats.ResetSpeed();
        _state = EMonsterState.Move;
        _bindCoroutine = null;
    }

    public void TakeDotDamage(int damage, float duration, float interval)
    {
        if (_stats.CurrentHealth <= 0) return;
        if (_dotDamageCoroutine != null)
        {
            StopCoroutine(_dotDamageCoroutine);
        }
        _dotDamageCoroutine = StartCoroutine(ProcessDotDamage(damage, duration, interval));
    }

    private IEnumerator ProcessDotDamage(int damage, float duration, float interval)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(interval);
            TakeDamage(damage);
            elapsedTime += interval;
        }
        _dotDamageCoroutine = null;
    }

    public void OnAnimationEnd()
    {
        _state = EMonsterState.Move;
        _animator.PlayMoveAnimation(true);
    }

    public void OnDeathAnimationEnd()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
}