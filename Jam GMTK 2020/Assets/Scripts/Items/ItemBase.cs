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
        }
        public virtual void Drop()
        {
            onDrop?.Invoke();
        }

        public virtual void Use(PlayerController source)
        {

        }
        public virtual void StopUse()
        {

        }
    }
}