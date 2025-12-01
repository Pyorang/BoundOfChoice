using UnityEngine;

public class ProjectileAttacker : MonoBehaviour
{
    [Header("투사체 발사 위치")]
    [SerializeField] private Transform _attackOffset;

    [Header("투사체 종류")]
    [SerializeField] private EPoolType _type;

    private void Shot()
    {
        int attackDirection = transform.right == Vector3.right ? 1 : -1;

        GameObject projectile = PoolManager.Instance.GetObject(_type);
        projectile.GetComponent<ProjectileBase>().Init(_attackOffset.position, attackDirection, 0);
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
