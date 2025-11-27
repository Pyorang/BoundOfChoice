using UnityEngine;

public class IceAgeSkill : SkillBase
{
    public override ESkillType SkillType => ESkillType.IceAge;

    [Header("공격 범위")]
    [Space]
    [SerializeField] private Vector2 _attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private Vector2 _boxOffset = new Vector2(1f, 0f);
    [SerializeField] private LayerMask _enemyLayer;

    [Header("스킬 효과")]
    [Space]
    [SerializeField] private float _bindDuration;

    protected override void ExecuteSkill(int direction, int additionalDamage = 0)
    {
        Vector2 boxPosition = PlayerHealth.Instance.gameObject.transform.position;
        boxPosition.x += _boxOffset.x * direction;

        Collider2D[] hitMonsters =
            Physics2D.OverlapBoxAll(boxPosition, _attackRange, 0.0f, _enemyLayer);

        foreach (Collider2D hitMonster in hitMonsters)
        {
            if (hitMonster.TryGetComponent<MonsterController>(out var monster))
            {
                monster.TakeBind(_bindDuration);
            }
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
