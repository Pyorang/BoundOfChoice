using UnityEngine;

public class SkeletonNecro : SkeletonSwordsman
{
    [Header("소환 공격 설정")]
    [Space]
    [SerializeField] private float _castInterval;

    private MonsterAnimator _animator;

    protected override void Init()
    {
        base.Init();
        _animator = GetComponent<MonsterAnimator>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Cast), _castInterval, _castInterval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Cast));
    }

    private void Cast()
    {
        _animator.PlaySpecialAttackAnimation();
    }

    public void SpawnTombstone()
    {
        GameObject tombstone = PoolManager.Instance.GetObject(EPoolType.Tombstone);

        Vector2 spawnPoint = PlayerMovement.Instance.transform.position;
        spawnPoint.y = tombstone.transform.position.y;

        tombstone.GetComponent<ProjectileBase>().Init(spawnPoint, 1, _stats.AttackPower);
    }
}
