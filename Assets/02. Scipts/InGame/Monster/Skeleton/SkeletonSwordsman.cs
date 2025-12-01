using UnityEngine;

public class SkeletonSwordsman : BaseMonster
{
    [Header("공격 범위")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector2 _attackBoxSize;

    [Header("레이어 설정")]
    [SerializeField] private LayerMask _targetLayer;

    public void OnAttackHit()
    {
        Collider2D hit = Physics2D.OverlapBox(_attackPoint.position, _attackBoxSize, 0f, _targetLayer);

        if (hit == null || !hit.CompareTag(PlayerTag)) return;
        PlayerHealth.Instance.TakeDamage(_stats.AttackPower);        
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(_attackPoint.position, _attackBoxSize);
    }
#endif
}
