using UnityEngine;

public abstract class InteractObjectBase : MonoBehaviour
{
    protected abstract void TriggerEnter(Collider2D other);
    public abstract void Execute();
    protected abstract void TriggerExit(Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other);
    }
}
