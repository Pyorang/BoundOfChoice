using System.Collections;
using UnityEngine;

public class SkeletonNecro : SkeletonSwordsman
{
    [Header("소환 공격 설정")]
    [Space]
    [SerializeField] private float _castInterval;

    private MonsterAnimator _animator;
    private WaitForSeconds _waitInterval;
    private Coroutine _castCoroutine;

    protected override void Init()
    {
        base.Init();
        _animator = GetComponent<MonsterAnimator>();
        _waitInterval = new WaitForSeconds(_castInterval);
    }

    private void OnEnable()
    {
        _castCoroutine = StartCoroutine(CastCoroutine());
    }

    private void OnDisable()
    {
        if (_castCoroutine == null) return;

        StopCoroutine(_castCoroutine);
        _castCoroutine = null;
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

    private IEnumerator CastCoroutine()
    {
        while (true)
        {
            yield return _waitInterval;
            Cast();
        }
    }
}
