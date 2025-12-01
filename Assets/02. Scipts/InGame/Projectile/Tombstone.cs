using UnityEngine;

public class Tombstone : ProjectileBase
{
    public override void ApplyDamage(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth.Instance.TakeDamage(_finalDamage);
    }

    public override void Move() { }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.Tombstone, this.gameObject);
    }
}
