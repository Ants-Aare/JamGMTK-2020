using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("References")]
    public List<Interactable> interactablesInRange = new List<Interactable>();
    private Interactable closestInteractable;
    [SerializeField]
    private InteractableUI interactableUI = null;
    private bool canInteract = true;

    public void CanInteract(bool value)
    {
        if (!value)
            interactableUI.SetActiveInteractable(null);
        canInteract = value;
    }

    void LateUpdate()
    {
        if (canInteract)
            UpdateClosestInteractable();
    }

    #region Region

    private void UpdateClosestInteractable()
    {
        if (interactablesInRange.Count > 0)
        {
            int index = 0;
            // loop over all of the interactables to find the nearest one. if there's just one, then that's automatically the closest
            if (interactablesInRange.Count > 1)
            {
                float nearestDistance = 10000f;
                for (int i = 0; i < interactablesInRange.Count; i++)
                {
                    float distance = (interactablesInRange[i].transform.position - gameObject.transform.position).sqrMagnitude;
                    if (distance <= nearestDistance)
                    {
                        nearestDistance = distance;
                        index = i;
                    }
                }
            }
            else
                index = 0;

            // the closest one has changed
            if (closestInteractable != interactablesInRange[index])
            {
                closestInteractable = interactablesInRange[index];
                if (interactableUI != null)
                    interactableUI.SetActiveInteractable(closestInteractable);
            }
        }
        else
        {
            if (closestInteractable != null)
            {
                closestInteractable = null;
                if (interactableUI != null)
                    interactableUI.SetActiveInteractable(null);
            }
        }
    }
    public void InteractWithNearest(InputAction.CallbackContext context)
    {
        if (context.started && canInteract)
        {
            if (closestInteractable != null)
            {
                Interactable nearest = closestInteractable;
                nearest.StartInteraction();
            }
        }
    }

    public void EndInteraction()
    {
        canInteract = true;
    }
    public void RemoveInteractable(Interactable interactable)
    {
        interactablesInRange.Remove(interactable);
        if (closestInteractable == interactable)
        {
            UpdateClosestInteractable();
        }
    }

    #endregion
}