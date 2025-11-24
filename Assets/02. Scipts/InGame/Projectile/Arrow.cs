using UnityEngine;

public class Arrow : ProjectileBase
{
    [Header("관통 개수")]
    private int _attackCount = 0;
    [SerializeField] private int _maxAttackCount = 4;

    public override void ApplyDamage(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        MonsterStats stat = other.GetComponent<MonsterStats>();
        if (stat == null) return;
        stat.TakeDamage(_damage);
        _attackCount++;
        if (_attackCount >= _maxAttackCount)
        {
            // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
            Destroy(this.gameObject);
        }
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }
}
