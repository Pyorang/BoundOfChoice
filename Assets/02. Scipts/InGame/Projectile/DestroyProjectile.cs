using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ProjectileBase projectile = other.GetComponent<ProjectileBase>();
        if (projectile == null) return;
        projectile.ReleaseObject();
    }
}
