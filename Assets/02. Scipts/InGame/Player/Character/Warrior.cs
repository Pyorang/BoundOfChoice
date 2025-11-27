using UnityEngine;

public class Warrior : CharacterBase
{
    [Header("공격 범위")]
    [SerializeField] private Vector2 _attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private Vector2 _boxOffset = new Vector2(1f, 0f);
    [SerializeField] private LayerMask _enemyLayer;

    [Header("데미지")]
    [SerializeField] private int _attackDamage = 50;

    public override void Attack(int direction, int additionalDamage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "Sword");

        Vector2 boxPosition = this.transform.position;
        boxPosition.x += _boxOffset.x * direction;

        Collider2D[] hitMonsters =
            Physics2D.OverlapBoxAll(transform.position, _attackRange, 0.0f, _enemyLayer);
        
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
    [SerializeField] private Vector2 _drawOffset = new Vector2(0f, -0.5f);
    public override void DrawRange(Vector2 position, int direction)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;
        boxPosition += _drawOffset;

        Gizmos.color = _drawColor;
        Gizmos.DrawCube(boxPosition, _attackRange);
    }
#endif
}