using UnityEngine;

public class FireBall : ProjectileBase
{
    private bool _isExploded = false;
    [SerializeField] private Animator _animator;

    [Header("Dot 데미지")]
    [Space]
    [SerializeField] private int _dotDamagePerTick = 10;
    [SerializeField] private float _dotDuration = 3.0f;
    [SerializeField] private float _dotTickInterval = 1.0f;

    public override void Init(Vector2 position, int direction, int additionalDamage = 0)
    {
        base.Init(position, direction, additionalDamage);
        _isExploded = false;
    }

    public override void ApplyDamage(Collider2D other)
    {
        if (_isExploded) return;
        if (!other.CompareTag("Enemy")) return;
        MonsterController monster = other.GetComponent<MonsterController>();
        if (monster == null) return;
        monster.TakeDotDamage(_dotDamagePerTick, _dotDuration, _dotTickInterval);
        monster.TakeDamage(_finalDamage);
        _isExploded = true;
        _animator.SetTrigger("Explode");
    }

    public override void Move()
    {
        if (_isExploded) return;
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.FireBall, this.gameObject);
    }
}
