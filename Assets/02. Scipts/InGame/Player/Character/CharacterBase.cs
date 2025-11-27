using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [Header("처음 플레이할 캐릭터일 경우 선택")]
    [Space]
    public bool GoingWith = false;

    [Header("캐릭터 타입")]
    [Space]
    [SerializeField] private ECharacterType _characterType;
    public ECharacterType CharacterType => _characterType;

    [Header("공격 쿨타임")]
    [Space]
    [SerializeField] protected float _attackCoolTime = 1.0f;
    private float _lastAttackTime;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init() { }

    public void DeactivateCharacter()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivateCharacter()
    {
        this.gameObject.SetActive(true);
    }

    public abstract void Attack(int direction, int additionalDamage);

    public bool CanAttack() => Time.time - _lastAttackTime >= _attackCoolTime;
    public void SaveLastAttackTime() => _lastAttackTime = Time.time;

#if UNITY_EDITOR
    public virtual void DrawRange(Vector2 position, int direction) { }
#endif
}
