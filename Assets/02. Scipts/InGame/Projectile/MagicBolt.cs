using UnityEngine;

public class MagicBolt : ProjectileBase
{
    public override void ApplyDamage(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;
        other.GetComponent<MonsterStats>()?.TakeDamage(_damage);

        // NOTE : 이후 Pooling 방식을 사용해 오브젝트 관리
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public void OnDestroy()
    {
        // Note : Pooling 방식 적용시 Bolt Counting 방식 수정
        Mage.BoltCount--;
    }
}
