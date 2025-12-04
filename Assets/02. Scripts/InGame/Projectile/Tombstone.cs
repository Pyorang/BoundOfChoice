using UnityEngine;

public class Tombstone : ProjectileBase
{
    private static readonly string TombstoneSound = "Tombstone";

    private void PlayTombstoneSound()
    {
        AudioManager.Instance.Play(AudioType.SFX, TombstoneSound);
    }

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
