using UnityEngine;

public class SkeletonArrow : ProjectileBase
{
    public override void ApplyDamage(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioManager.Instance.Play(AudioType.SFX, "ArrowHit");
        PlayerHealth.Instance.TakeDamage(_damage);
        ReleaseObject();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.SkeletonArrow, this.gameObject);
    }
}
