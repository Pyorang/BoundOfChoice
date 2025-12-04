using UnityEngine;

public class DashAttacker : MeleeAttacker
{
    private bool _isDash = false;

    private static readonly string _dashSound = "SkeletonDash";
    private static readonly string _crashSound = "SkeletonCrash";

    public void StartDash()
    {
        AudioManager.Instance.Play(AudioType.SFX, _dashSound);
        _isDash = true;
    }

    public void EndDash()
    {
        _isDash = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isDash || !other.gameObject.CompareTag(PlayerTag)) return;
        AudioManager.Instance.Play(AudioType.SFX, _crashSound);
        PlayerHealth.Instance.TakeDamage(_stats.AttackPower);
    }
}
