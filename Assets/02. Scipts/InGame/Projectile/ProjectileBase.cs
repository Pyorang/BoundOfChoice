using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [Header("공격")]
    [SerializeField] protected float _damage;

    [Header("이동")]
    protected int _direction = -1;
    [SerializeField] protected float _speed = 10.0f;

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

    public void SetProjectileInfo(Vector2 position, int direction, float damage)
    {
        this.transform.position = position;
        _direction = direction;
        _renderer.flipX = (direction < 0);
        _damage = damage;
    }

    public abstract void ReleaseObject();

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamage(other);
    }
}
