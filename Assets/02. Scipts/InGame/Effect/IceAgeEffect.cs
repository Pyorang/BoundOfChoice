using UnityEngine;

public class IceAgeEffect : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;

    public void SetEffect(Vector2 position, int direction)
    {
        this.transform.position = position;
        _renderer.flipX = (direction < 0);
    }

    private void ReleaseEffect()
    {
        PoolManager.Instance.ReleaseObject(EPoolType.IceAgeEffect, this.gameObject);
    }
}
