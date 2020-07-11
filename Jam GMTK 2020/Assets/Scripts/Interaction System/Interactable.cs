using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour
{
    [Header("Values")]
    public string actionName = "Interact";
    [SerializeField]
    private bool canInteract = true;
    [SerializeField]
    private bool awaitInteractionEnd = false;
    public Transform UITarget = null;
    private InteractionController controller;

    [Header("Events")]
    public UnityEvent onInteractionStart = null;
    public UnityEvent onInteractionEnd = null;

    void Start()
    {
        CanInteract(canInteract);
    }
    private void OnDisable()
    {
        if (controller != null)
            controller.RemoveInteractable(this);
    }
    void OnDestroy()
    {
        if (controller != null)
            controller.RemoveInteractable(this);
    }

    #region Collision Registering
    void OnTriggerEnter(Collider collider)
    {
        if (canInteract)
        {
            InteractionController interactionController = collider.gameObject.GetComponent<InteractionController>();
            if (interactionController != null)
            {
                if (!interactionController.interactablesInRange.Contains(this))
                    interactionController.interactablesInRange.Add(this);
                controller = interactionController;
            }
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (canInteract)
        {
            InteractionController interactionController = collider.gameObject.GetComponent<InteractionController>();
            if (interactionController != null)
            {
                interactionController.RemoveInteractable(this);
            }
        }
    }
    #endregion

    #region Interacting

    public void StartInteraction()
    {
        onInteractionStart?.Invoke();
        if (awaitInteractionEnd)
        {
            controller.CanInteract(false);
        }
    }
    public void EndInteraction()
    {
        onInteractionEnd?.Invoke();
        controller?.CanInteract(true);
    }
    public void CanInteract(bool value)
    {
        canInteract = value;
        // no need to add it once can interact is enabled, since ontriggerenter is called when activating the collider
        if (!value)
        {
            controller?.RemoveInteractable(this);
        }
        GetComponent<SphereCollider>().enabled = value;
    }

    #endregion
}