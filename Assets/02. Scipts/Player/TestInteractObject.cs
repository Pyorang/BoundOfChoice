using UnityEngine;

public class TestInteractObject : InteractObjectBase
{
    protected override void TriggerEnter(Collider2D other)
    {
        other.GetComponent<PlayerInteraction>()?.AddInteractableObject(this, transform);
    }

    public override void Execute()
    {
#if UNITY_EDITOR
        Debug.Log($"Interact with {gameObject.name}");
#endif
    }

    protected override void TriggerExit(Collider2D other)
    {
        other.GetComponent<PlayerInteraction>()?.RemoveInteractableObject(this, transform);
    }
}
