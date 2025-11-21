using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    private List<IInteractable> _interactableObjects = new List<IInteractable>();

    private void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactObject = GetNearestObject();
            if (interactObject == null) return;
            interactObject.Execute();
        }
    }

    private IInteractable GetNearestObject()
    {
        IInteractable nearest = null;
        float minDistance = float.MaxValue;
        Vector3 myPosition = transform.position;

        for (int i = _interactableObjects.Count - 1; i >= 0; i--)
        {
            MonoBehaviour monoObject = _interactableObjects[i] as MonoBehaviour;
            if (monoObject == null)
            {
                _interactableObjects.RemoveAt(i);
                continue;
            }

            float distance = (monoObject.transform.position - myPosition).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = _interactableObjects[i];
            }
        }
        return nearest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactableObject = other.GetComponent<IInteractable>();
        if (interactableObject == null) return;
        _interactableObjects.Add(interactableObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactableObject = other.GetComponent<IInteractable>();
        if (interactableObject == null) return;
        _interactableObjects.Remove(interactableObject);
    }
}
