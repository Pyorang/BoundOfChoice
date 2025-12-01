using System.Collections;
using UnityEngine;

public class BringerOfDeath : SkeletonSwordsman
{
    [Header("범위 공격 설정")]
    [Space]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
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

    private void CastSpell()
    {
        GameObject spell = PoolManager.Instance.GetObject(EPoolType.Spell);

        Vector2 spawnPoint = transform.position;
        spawnPoint.x = Random.Range(_minX, _maxX);

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, _stats.AttackPower);
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
