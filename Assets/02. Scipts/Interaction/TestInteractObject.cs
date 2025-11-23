using UnityEngine;

public class TestInteractObject : InteractObjectBase
{
    public override void Execute()
    {
#if UNITY_EDITOR
        Debug.Log($"Interact with {gameObject.name}");
#endif
    }
}
