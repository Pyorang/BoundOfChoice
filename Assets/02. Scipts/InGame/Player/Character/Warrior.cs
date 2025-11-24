using System.Drawing;
using UnityEngine;

public class Warrior : CharacterBase
{
    [Header("공격 범위")]
    [SerializeField] private Vector2 _attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private Vector2 _boxOffset = new Vector2(1f, 0f);

    public override void Attack(Vector2 position, float power, int direction)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;

        Collider2D[] hitMonsters =
            Physics2D.OverlapBoxAll(boxPosition, _attackRange, 0.0f, LayerMask.GetMask("Enemy"));
        
        foreach (Collider2D hit in hitMonsters)
        {
            MonsterStats monster = hit.GetComponent<MonsterStats>();
            if (monster == null) continue;
            monster.TakeDamage(power * _attackDamage);
        }
    }

    public override void DrawRange(Vector2 position, int direction)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawCube(boxPosition, _attackRange);
    }
}