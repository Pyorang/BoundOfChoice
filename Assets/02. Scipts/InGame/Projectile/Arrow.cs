using UnityEngine;

public class Arrow : ProjectileBase
{
    public override void ApplyDamage(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        MonsterController stat = other.GetComponent<MonsterController>();
        if (stat == null) return;
        stat.TakeDamage(_damage);
        ReleaseObject();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.Arrow, this.gameObject);
    }
}
