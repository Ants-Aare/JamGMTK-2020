using UnityEngine;
using UnityEngine.Events;

namespace GMTKJAM.Items
{
    public class ToolItem : ItemBase
    {
        public Rigidbody rigidBody;
        void Awake()
        {
            if (rigidBody == null)
                rigidBody = GetComponent<Rigidbody>();
            if (rigidBody == null)
                Debug.LogError("Please Add a Rigidbody Component");
        }
        public override void PickUp()
        {
            onPickup?.Invoke();

            Destroy(rigidBody);

            PlayerController.instance.inventoryController.EquipItem(this);
        }
        public override void Use(PlayerController source)
        {
        }

        public virtual void Select()
        {

        }
        public virtual void Deselect()
        {

        }
    }
}