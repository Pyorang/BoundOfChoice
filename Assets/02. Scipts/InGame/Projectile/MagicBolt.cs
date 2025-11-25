using UnityEngine;

public class MagicBolt : ProjectileBase
{
    public override void ApplyDamage(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        other.GetComponent<MonsterStats>()?.TakeDamage(_damage);
        ReleaseObject();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.MagicBolt, this.gameObject);
    }
}
