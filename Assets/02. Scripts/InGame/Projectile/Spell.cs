using UnityEngine;

public class Spell : ProjectileBase
{
    [Header("타입 설정")]
    [SerializeField] private EPoolType _poolType;

    [Header("사운드 설정")]
    [SerializeField] private string _spellSound = "Spell";

    private void PlaySpellSound()
    {
        AudioManager.Instance.Play(AudioType.SFX, _spellSound);
    }

    public override void ApplyDamage(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerHealth.Instance.TakeDamage(_finalDamage);
    }

    public override void Move() 
    {
        // NOTE : 마법진이 플레이어를 따라다님 -> 고정 위치에서 공격하는 것으로 변경
        /*
        if (_direction == 0) return;

        float distanceX = _target.position.x - transform.position.x;
        if (Mathf.Approximately(distanceX, 0f)) return;

        _direction = distanceX > 0f ? 1 : -1;
        transform.Translate(Vector2.right * (_direction * _speed * Time.deltaTime));
        */
    }

    public override void ReleaseObject()
    {
        PoolManager.Instance.ReleaseObject(_poolType, this.gameObject);
    }
}
