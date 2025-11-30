using UnityEngine;

public class SkeletonElite : SkeletonSwordsman
{
    private bool isDash = false;

    public void StartDash()
    {
        isDash = true;
    }

    public void EndDash()
    {
        isDash = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDash || !other.gameObject.CompareTag(PlayerTag)) return;
        PlayerHealth.Instance.TakeDamage(_stats.AttackPower);
    }
}
