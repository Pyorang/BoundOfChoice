using UnityEngine;

public abstract class MonsterAttacker : MonoBehaviour
{
    protected MonsterStats _stats;
    protected SpriteRenderer _spriteRenderer;

    protected const string PlayerTag = "Player";

    private void Awake()
    {
        _stats = GetComponent<MonsterStats>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
