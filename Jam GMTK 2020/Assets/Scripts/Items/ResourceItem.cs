using UnityEngine;

namespace GMTKJAM.Items
{
    public class ResourceItem : ItemBase
    {
        public Rigidbody rigidBody;
        private float defaultDrag = 0.3f;
        private float carryDrag = 4f;
        public int resourceAmount = 50;

        void Awake()
        {
            if (rigidBody == null)
                rigidBody = GetComponent<Rigidbody>();
            if (rigidBody == null)
                Debug.LogError("Please Add a Rigidbody Component");
        }

        void Start()
        {
            rigidBody.useGravity = true;
            rigidBody.drag = defaultDrag;
        }
        public override void PickUp()
        {
            onPickup?.Invoke();

            rigidBody.useGravity = false;
            rigidBody.drag = carryDrag;

            PlayerController.instance.inventoryController.StartCarryItem(this);
        }
        public override void Drop()
        {
            base.Drop();
            rigidBody.useGravity = true;
            rigidBody.drag = defaultDrag;
        }
        public override void Use(InventoryController source)
        {
            source.ThrowItem();
        }
    }
}