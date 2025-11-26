using UnityEngine;

public class Warrior : CharacterBase
{
    public override ECharacterType CharacterType => ECharacterType.Warrior;
    [Header("공격 범위")]
    [SerializeField] private Vector2 _attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private Vector2 _boxOffset = new Vector2(1f, 0f);
    [SerializeField] private LayerMask _enemyLayer;

    [Header("데미지")]
    [SerializeField] private int _attackDamage = 25;

    public override void Attack(int direction, int additionalDamage)
    {
        Vector2 boxPosition = this.transform.position;
        boxPosition.x += _boxOffset.x * direction;

        Collider2D[] hitMonsters =
            Physics2D.OverlapBoxAll(boxPosition, _attackRange, 0.0f, _enemyLayer);
        
        foreach (Collider2D hitMonster in hitMonsters)
        {
            if (hitMonster.TryGetComponent<MonsterStats>(out var monster))
            {
                monster.TakeDamage(additionalDamage + _attackDamage);
            }
        }
    }

#if UNITY_EDITOR
    public override void DrawRange(Vector2 position, int direction)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;
        boxPosition.y -= 0.5f;

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawCube(boxPosition, _attackRange);
    }
#endif
}