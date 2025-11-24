using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _arrowSpeed = 10.0f;
    private int _direction = -1;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = 
            (Vector2)this.transform.position + Vector2.right * _direction * _arrowSpeed * Time.deltaTime;
    }

    public void SetArrowInfo(int direction, float damage)
    {
        _direction = direction;
        _renderer.flipY = (direction < 0);
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        other.GetComponent<MonsterStats>()?.TakeDamage(_damage);
        Destroy(this.gameObject);
    }
}
