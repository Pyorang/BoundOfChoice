using UnityEngine;

public class ArrowProjectile : ProjectileBase
{
    [Header("설정")]
    [Space]
    [Tooltip("이 투사체가 맞춰야 할 대상의 태그")]
    [SerializeField] private string _targetTag;

    [Tooltip("오브젝트 풀 반환 타입")]
    [SerializeField] private EPoolType _poolType;

    public override void ApplyDamage(Collider2D other)
    {
        if (!other.CompareTag(_targetTag)) return;

        AudioManager.Instance.Play(AudioType.SFX, "ArrowHit");

        if (_targetTag == "Player")
        {
            PlayerHealth.Instance.TakeDamage(_finalDamage);
        }
        else if(_targetTag == "Enemy")
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (monster == null) return;
            if (!monster.TakeDamage(_finalDamage)) return;
        }
        ReleaseObject();
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(_poolType, this.gameObject);
    }
}
