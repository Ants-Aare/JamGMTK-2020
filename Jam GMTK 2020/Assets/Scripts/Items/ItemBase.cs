using UnityEngine;
using UnityEngine.Events;

namespace GMTKJAM.Items
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField]
        protected UnityEvent onPickup;
        [SerializeField]
        protected UnityEvent onDrop;

        public virtual void PickUp()
        {
            onPickup?.Invoke();
            PlayerController.instance.inventoryController.EquipItem(this);
        }
        public virtual void Drop()
        {
            onDrop?.Invoke();
        }
        public virtual void Use(InventoryController source)
        {

        }
    }
}