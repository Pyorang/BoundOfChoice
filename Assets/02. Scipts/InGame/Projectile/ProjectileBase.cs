using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [Header("공격")]
    [Space]
    [SerializeField] private int _cost;
    [SerializeField] protected int _damage;

    [Header("이동")]
    [Space]
    [SerializeField] protected float _speed = 10.0f;
    protected int _direction = -1;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    public abstract void ApplyDamage(Collider2D other);
    public abstract void Move();
    public abstract void ReleaseObject();

    public bool TryConsumeCost()
    {
        if (PlayerMana.Instance.TryUseMana(_cost)) return true;
        ReleaseObject();
        return false;
    }

    public void Init(Vector2 position, int direction, int additionalDamage = 0)
    {
        this.transform.position = position;
        _direction = direction;
        _renderer.flipX = (direction < 0);
        _damage += additionalDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamage(other);
    }
}
