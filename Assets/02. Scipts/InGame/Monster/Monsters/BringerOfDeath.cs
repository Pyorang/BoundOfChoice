using UnityEngine;

public class BringerOfDeath : SkeletonSwordsman
{
    [Header("범위 공격 설정")]
    [Space]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
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

    private void CastSpell()
    {
        GameObject spell = PoolManager.Instance.GetObject(EPoolType.Spell);

        Vector2 spawnPoint = transform.position;
        spawnPoint.x = Random.Range(_minX, _maxX);

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, _stats.AttackPower);
    }
}
