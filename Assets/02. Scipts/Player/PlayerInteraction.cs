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
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractObjectBase targetObject = UpdateTarget();
            if (targetObject == null) return;
            targetObject.Execute();
        }
    }

    public void AddInteractableObject(InteractObjectBase interactableObject)
    {
        if (interactableObject == null) return; 
        if (_interactableObjects.Contains(interactableObject)) return;
        _interactableObjects.Add(interactableObject);
    }

    public void RemoveInteractableObject(InteractObjectBase interactableObject)
    { 
        if (_interactableObjects.Contains(interactableObject) == false) return;
        _interactableObjects.Remove(interactableObject);
    }

    private float GetDistance(Transform other)
    {
        float distance = (other.position - transform.position).sqrMagnitude;
        return distance;
    }

    private InteractObjectBase UpdateTarget()
    {
        InteractObjectBase target = null;
        float minDistance = float.MaxValue;
        foreach (var interactableObject in _interactableObjects)
        {
            Transform transform = interactableObject.transform;
            float currentDistance = GetDistance(transform);
            if (currentDistance < minDistance)
            {
                target = interactableObject;
                minDistance = currentDistance;
            }
        }
        return target;
    }
}
