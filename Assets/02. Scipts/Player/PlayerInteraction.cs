using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<(InteractObjectBase, Transform)> _interactableObjects = new List<(InteractObjectBase, Transform)>();

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

    public void AddInteractableObject(InteractObjectBase interactableObject, Transform transform)
    {
        if (interactableObject == null) return; 
        var objectTuple = (interactableObject, transform);
        if (_interactableObjects.Contains(objectTuple)) return;
        _interactableObjects.Add(objectTuple);
    }

    public void RemoveInteractableObject(InteractObjectBase interactableObject, Transform transform)
    {
        var objectTuple = (interactableObject, transform);
        if (_interactableObjects.Contains(objectTuple) == false) return;
        _interactableObjects.Remove(objectTuple);
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
            float currentDistance = GetDistance(interactableObject.Item2);
            if (currentDistance < minDistance)
            {
                target = interactableObject.Item1;
                minDistance = currentDistance;
            }
        }
        return target;
    }
}
