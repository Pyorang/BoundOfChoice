using UnityEngine;

public class SkeletonNecro : SkeletonSwordsman
{
    private MonsterAnimator _animator;

    protected override void Init()
    {
        base.Init();
        _animator = GetComponent<MonsterAnimator>();
    }

    public void SpawnTombstone()
    {
        GameObject tombstone = PoolManager.Instance.GetObject(EPoolType.Tombstone);

        Vector2 spawnPoint = PlayerMovement.Instance.transform.position;
        spawnPoint.y = tombstone.transform.position.y;

        tombstone.GetComponent<ProjectileBase>().Init(spawnPoint, 1, _stats.AttackPower);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(PlayerTag)) return;
        _animator.PlayAttackAnimation();
    }
}
