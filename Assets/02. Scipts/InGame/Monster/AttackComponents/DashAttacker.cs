using UnityEngine;

public class DashAttacker : MeleeAttacker
{
    private bool _isDash = false;

    public void StartDash()
    {
        _isDash = true;
    }

    public void EndDash()
    {
        _isDash = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isDash || !other.gameObject.CompareTag(PlayerTag)) return;
        PlayerHealth.Instance.TakeDamage(_stats.AttackPower);
    }
}
