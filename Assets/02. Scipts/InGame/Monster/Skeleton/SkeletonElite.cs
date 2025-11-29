using UnityEngine;
using UnityEngine.Timeline;

public class SkeletonElite : BaseMonster
{
    [Header("공격 범위")]
    [SerializeField] private Vector2 _attackOffset;
    [SerializeField] private Vector2 _attackBoxSize;

    [Header("레이어 설정")]
    [SerializeField] private LayerMask _targetLayer;

    public void OnAttackHit()
    {
        int attackDirection = _spriteRenderer.flipX ? -1 : 1;

        Vector2 attackOffset = _attackOffset;
        attackOffset.x *= attackDirection;

        Vector2 attackPoint = (Vector2)transform.position + attackOffset;

        Collider2D hit = Physics2D.OverlapBox(attackPoint, _attackBoxSize, 0f, _targetLayer);

        if (hit == null || !hit.CompareTag(PlayerTag)) return;
        PlayerHealth.Instance.TakeDamage(_stats.AttackPower);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        int attackDirection = _spriteRenderer.flipX ? -1 : 1;

        Vector2 attackOffset = _attackOffset;
        attackOffset.x *= attackDirection;

        Vector2 attackPoint = (Vector2)transform.position + attackOffset;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(attackPoint, _attackBoxSize);
    }
#endif
}
