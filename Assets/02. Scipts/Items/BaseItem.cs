using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        ApplyEffect();
    }

    protected abstract void ApplyEffect();
}