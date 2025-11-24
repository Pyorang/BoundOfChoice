using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [Header("공격")]
    [SerializeField] protected float _damage;

    [Header("이동")]
    protected int _direction = -1;
    [SerializeField] protected float _speed = 10.0f;
    [SerializeField] private float _destroyTime = 5.0f;

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

    public void SetProjectileInfo(int direction, float damage)
    {
        _direction = direction;
        _renderer.flipX = (direction < 0);
        _damage = damage;

        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        Destroy(gameObject, _destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamage(other);
    }
}
