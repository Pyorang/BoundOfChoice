using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileBase projectile = collision.gameObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        projectile.ReleaseObject();
    }
}
