using UnityEngine;
using UnityEngine.UIElements;

public class IceAge : MonoBehaviour
{
    [Header("공격 범위")]
    [Space]
    [SerializeField] private Vector2 _attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private Vector2 _boxOffset = new Vector2(1f, 0f);
    [SerializeField] private LayerMask _enemyLayer;


    [Header("스킬 밸런스")]
    [Space]
    [SerializeField] private float _cost;
    [SerializeField] private float _bindDuration;

    public void UseSkill(Vector2 position, int direction, int additionalDamage = 0)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;

        Collider2D[] hitMonsters =
            Physics2D.OverlapBoxAll(boxPosition, _attackRange, 0.0f, _enemyLayer);

        foreach (Collider2D hitMonster in hitMonsters)
        {
            if (hitMonster.TryGetComponent<MonsterStats>(out var monster))
            {
                monster.TakeBind(_bindDuration);
            }
        }
    }

#if UNITY_EDITOR
    public void DrawRange(Vector2 position, int direction)
    {
        Vector2 boxPosition = position;
        boxPosition.x += _boxOffset.x * direction;
        boxPosition.y -= 0.5f;

        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawCube(boxPosition, _attackRange);
    }
#endif
}
