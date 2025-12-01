using UnityEngine;

public class SkeletonArbalist : BaseMonster
{
    [Header("화살 발사 위치")]
    [SerializeField] private Transform _attackOffset;

    public void ShotArrow()
    {
        int attackDirection = transform.right == Vector3.right ? 1 : -1;

        GameObject arrow = PoolManager.Instance.GetObject(EPoolType.SkeletonArrow);
        arrow.GetComponent<ProjectileBase>().Init(_attackOffset.position, attackDirection, _stats.AttackPower);
    }

#if UNITY_EDITOR
private void OnDrawGizmosSelected()
{
    Vector2 attackOffset= _attackOffset.position;
    attackOffset.y += 1.25f;

    Gizmos.color = Color.yellow;

    Gizmos.DrawSphere(attackOffset, 0.2f);
}
#endif
}
