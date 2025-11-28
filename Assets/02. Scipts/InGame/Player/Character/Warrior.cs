using UnityEngine;

public class Warrior : CharacterBase
{
    [Header("공격 범위")]
    [SerializeField] private float _attackRange = 2.5f;
    private float _attackHeight = 0.1f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("데미지")]
    [SerializeField] private int _attackDamage = 50;

    public override void Attack(int direction, int additionalDamage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "Sword");
        Vector2 attackStart = this.transform.position;
        Vector2 attackEnd = attackStart + new Vector2(direction * _attackRange, _attackHeight);
        Collider2D[] hitMonsters = Physics2D.OverlapAreaAll(attackStart, attackEnd, _enemyLayer);
        
        foreach (Collider2D hitMonster in hitMonsters)
        {
            if (hitMonster.TryGetComponent<MonsterController>(out var monster))
            {
                monster.TakeDamage(_attackDamage + additionalDamage);
            }
        }
    }

#if UNITY_EDITOR

    [Header("Gizmos 그리기 도구")]
    [Space]
    [SerializeField] private Color _drawColor = Color.red;
    public override void DrawRange(Vector2 position, int direction)
    {
        Vector2 attackStart = position;
        Vector2 attackEnd = attackStart + new Vector2(direction * _attackRange, 0);

        Vector2 center = (attackStart + attackEnd) / 2f;
        Vector2 size = new Vector2(_attackRange, _attackHeight);

        Gizmos.color = _drawColor;
        Gizmos.DrawCube(center, size);
    }
#endif
}