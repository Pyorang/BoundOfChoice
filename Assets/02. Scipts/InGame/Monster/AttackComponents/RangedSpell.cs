using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    [Header("범위 공격 설정")]
    [Space]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    [Header("투사체 타입")]
    [Space]
    [SerializeField] private EPoolType _poolType;

    private void CastSpell()
    {
        GameObject spell = PoolManager.Instance.GetObject(_poolType);

        Vector2 spawnPoint = transform.position;
        spawnPoint.x = Random.Range(_minX, _maxX);

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, 0);
    }

    private void CastSpellAtPlayerPosition()
    {
        if (PlayerHealth.Instance.IsDeath)
        {
            CastSpell();
            return;
        }

        GameObject spell = PoolManager.Instance.GetObject(_poolType);

        Vector2 spawnPoint = PlayerMovement.Instance.transform.position;
        spawnPoint.y = spell.transform.position.y;

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, 0);
    }
}
