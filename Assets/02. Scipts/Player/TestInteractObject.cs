using UnityEngine;

public class TestInteractObject : MonoBehaviour, IInteractable
{
    public void Execute()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
