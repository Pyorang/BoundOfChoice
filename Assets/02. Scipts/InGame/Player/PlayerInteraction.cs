using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<InteractObjectBase> _interactableObjects = new List<InteractObjectBase>();

    private void Update()
    {
        GetKeyInput();
    }

    private void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InteractObjectBase targetObject = UpdateNearestTarget();
            if (targetObject == null) return;
            targetObject.GetItem();
        }
    }

    public void AddInteractableObject(InteractObjectBase interactableObject)
    {
        _interactableObjects.Add(interactableObject);
    }

    public void RemoveInteractableObject(InteractObjectBase interactableObject)
    {
        _interactableObjects.Remove(interactableObject);
    }

    private float GetDistance(Transform other)
    {
        return (other.position - transform.position).sqrMagnitude;
    }

    private InteractObjectBase UpdateNearestTarget()
    {
        InteractObjectBase target = null;
        float minDistance = float.MaxValue;
        foreach (var interactableObject in _interactableObjects)
        {
            float currentDistance = GetDistance(interactableObject.transform);
            if (currentDistance < minDistance)
            {
                target = interactableObject;
                minDistance = currentDistance;
            }
        }
        return target;
    }
}
