using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _arrowSpeed = 10.0f;
    [SerializeField] private float _destroyTime = 5.0f;
    private int _direction = -1;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (_direction * _arrowSpeed * Time.deltaTime));
    }

    public void SetArrowInfo(int direction, float damage)
    {
        _direction = direction;
        _renderer.flipX = (direction < 0);
        _damage = damage;
        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        Destroy(gameObject, _destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        other.GetComponent<MonsterStats>()?.TakeDamage(_damage);

        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        Destroy(this.gameObject);
    }
}
