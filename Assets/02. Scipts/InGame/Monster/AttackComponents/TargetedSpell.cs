using UnityEngine;

public class TargetedSpell : MonoBehaviour
{
    private void CastSpell()
    {
        GameObject spell = PoolManager.Instance.GetObject(EPoolType.Tombstone);

        Vector2 spawnPoint = PlayerMovement.Instance.transform.position;
        spawnPoint.y = spell.transform.position.y;

        spell.GetComponent<ProjectileBase>().Init(spawnPoint, 1, 0);
    }
}
