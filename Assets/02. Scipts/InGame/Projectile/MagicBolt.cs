using UnityEngine;

public class MagicBolt : ProjectileBase
{
    [SerializeField] private float _splashDistance = 2.0f;
    [SerializeField] private float _splashDamageReduction = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;

    public override void ApplyDamage(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        other.GetComponent<MonsterController>()?.TakeDamage(_damage);

        Collider2D[] nearEnemies =
            Physics2D.OverlapCircleAll(this.transform.position, _splashDistance, _enemyLayer);

        foreach (var nearEnemy in nearEnemies)
        {
            if (nearEnemy == other) continue;
            nearEnemy.GetComponent<MonsterController>()?.TakeDamage(Mathf.RoundToInt(_damage * _splashDamageReduction));
        }

        ReleaseObject();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.MagicBolt, this.gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _splashDistance);
    }
#endif
}
