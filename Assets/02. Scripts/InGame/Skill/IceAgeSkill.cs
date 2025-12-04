using UnityEngine;

public class IceAgeSkill : SkillBase
{
    public override ESkillType SkillType => ESkillType.IceAge;

    [Header("공격 범위")]
    [Space]
    [SerializeField] private float _attackRange = 8.0f;
    private float _attackHeight = 0.1f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("스킬 효과")]
    [Space]
    [SerializeField] private float _bindDuration;

    protected override void ExecuteSkill(int direction, int additionalDamage = 0)
    {
        AudioManager.Instance.Play(AudioType.SFX, "IceAge");

        Vector2 attackStart = PlayerHealth.Instance.gameObject.transform.position;
        Vector2 attackEnd = attackStart + new Vector2(direction * _attackRange, _attackHeight);
        Collider2D[] hitMonsters = Physics2D.OverlapAreaAll(attackStart, attackEnd, _enemyLayer);

        foreach (Collider2D hitMonster in hitMonsters)
        {
            if (hitMonster.TryGetComponent<MonsterController>(out var monster))
            {
                monster.TakeBind(_bindDuration);
            }
        }

        GameObject skillEffectObject = PoolManager.Instance.GetObject(EPoolType.IceAgeEffect);
        if (skillEffectObject == null) return;
        if (skillEffectObject.TryGetComponent(out IceAgeEffect skillEffect))
        {
            Vector2 effectPosition = (attackStart + attackEnd) / 2.0f;
            skillEffect.SetEffect(effectPosition, direction);
        }
    }

/*#if UNITY_EDITOR
    [Header("Gizmos 그리기 도구")]
    [Space]
    [SerializeField] private Color _drawColor = Color.blue;
    [SerializeField] private Vector2 _drawOffset = new Vector2(0f, -0.5f);
    public void DrawRange(Vector2 position, int direction)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;
        boxPosition += _drawOffset;

        Gizmos.color = _drawColor;
        Gizmos.DrawCube(boxPosition, _attackRange);
    }
#endif*/
}
