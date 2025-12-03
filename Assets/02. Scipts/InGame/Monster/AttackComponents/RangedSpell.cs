using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    [Header("범위 공격 설정")]
    [Space]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private void CastSpell()
    {
        GameObject spell = PoolManager.Instance.GetObject(EPoolType.Spell);

        Vector2 spawnPoint = transform.position;
        spawnPoint.x = Random.Range(_minX, _maxX);

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, 0);
    }

    private void CastSpellAtPlayerPosition()
    {
        GameObject spell = PoolManager.Instance.GetObject(EPoolType.Spell);

        Vector2 spawnPoint = PlayerMovement.Instance.transform.position;
        spawnPoint.y = spell.transform.position.y;

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, 0);
    }
}
