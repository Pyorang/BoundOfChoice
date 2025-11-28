using UnityEngine;
using UnityEngine.Timeline;

public class SkeletonArbalist : BaseMonster
{
    [Header("화살 발사 위치")]
    [SerializeField] private Vector2 _attackOffset;

    public void ShotArrow()
    {
        int attackDirection = _spriteRenderer.flipX ? -1 : 1;

        Vector2 attackOffset = _attackOffset;
        attackOffset.x *= attackDirection;

        Vector2 attackPoint = (Vector2)transform.position + attackOffset;

        GameObject arrow = PoolManager.Instance.GetObject(EPoolType.SkeletonArrow);
        arrow.GetComponent<ProjectileBase>().Init(attackPoint, attackDirection, 0);
    }
}
