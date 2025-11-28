using UnityEngine;

public class Warrior : CharacterBase
{
    [Header("공격 범위")]
    [SerializeField] private Vector2 _attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private Vector2 _hitBoxOffset = new Vector2(1f, 0f);
    [SerializeField] private LayerMask _enemyLayer;

    [Header("데미지")]
    [SerializeField] private int _attackDamage = 50;

    public override void Attack(int direction, int additionalDamage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "Sword");

        Vector2 hitBoxPosition = this.transform.position;
        hitBoxPosition.x += _hitBoxOffset.x * direction;

        Collider2D[] hitMonsters =
            Physics2D.OverlapBoxAll(hitBoxPosition, _attackRange, 0.0f, _enemyLayer);
        
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
        Vector2 boxPosition = position;
        boxPosition.x += _hitBoxOffset.x * direction;

        Gizmos.color = _drawColor;
        Gizmos.DrawCube(boxPosition, _attackRange);
    }
#endif
}