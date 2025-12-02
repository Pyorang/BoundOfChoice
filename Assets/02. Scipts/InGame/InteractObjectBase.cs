using UnityEngine;

public abstract class InteractObjectBase : MonoBehaviour
{
    public abstract void GetItem();
    private PlayerInteraction _player = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_player != null) return;

        _player = other.GetComponent<PlayerInteraction>();
        _player?.AddInteractableObject(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_player == null) return;

        _player.RemoveInteractableObject(this);
        _player = null;
    }
}