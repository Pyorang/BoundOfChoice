using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileBase projectile = collision.gameObject.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        projectile.ReleaseObject();
    }
}
