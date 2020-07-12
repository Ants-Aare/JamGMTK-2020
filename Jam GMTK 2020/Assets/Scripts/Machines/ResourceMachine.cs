using UnityEngine;
using GMTKJAM.Items;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GMTKJAM.Machines
{
    public class ResourceMachine : MachineBase
    {
        protected List<ResourceItem> storedResources = new List<ResourceItem>();
        protected ResourceItem activeResource;
        [SerializeField]
        protected UnityEvent onResourceAdded;
        [SerializeField]
        protected UnityEvent onResourceEmpty;

        protected virtual void AddResource(ResourceItem resource)
        {
            onResourceAdded?.Invoke();
            storedResources.Add(resource);
            UpdateStat();
            resource.GetComponent<Interactable>().CanInteract(false);
        }
        protected virtual void RemoveResource(ResourceItem resource)
        {
            storedResources.Remove(resource);
            UpdateStat();
            resource.GetComponent<Interactable>().CanInteract(false);
        }

        public virtual bool UseResource()
        {
            if (activeResource == null)
            {
                if (storedResources.Count == 0)
                {
                    onResourceEmpty?.Invoke();
                    // We cannot use this resource
                    return false;
                }
                else
                {
                    activeResource = storedResources[storedResources.Count - 1];
                }
            }

            activeResource.resourceAmount--;

            if (activeResource.resourceAmount <= 0)
            {
                storedResources.Remove(activeResource);
                activeResource.DestroyItem();
                activeResource = null;
            }
            UpdateStat();
            return true;
        }

        public virtual void UpdateStat()
        {
            if (activeResource == null)
            {
                if (storedResources.Count >= 1)
                    stat.UpdateFillPercent(1);
                else
                    stat.UpdateFillPercent(0);
            }
            else
            {
                if (storedResources.Count > 1)
                    stat.UpdateFillPercent(1);
                else
                    stat.UpdateFillPercent(activeResource.resourceAmount / activeResource.totalResourceAmount);
            }

        }
    }
}