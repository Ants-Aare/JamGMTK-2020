using UnityEngine;
using UnityEngine.Events;

namespace GMTKJAM.Items
{
    public class ResourceItem : ItemBase
    {
        [SerializeField]
        private UnityEvent onDestroy;
        public int resourceAmount = 50;
        public int totalResourceAmount = 50;
        public Rigidbody rigidBody;
        private const float defaultDrag = 0.3f;
        private const float carryDrag = 4f;

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
        public override void Use(PlayerController source)
        {
            source.inventoryController.ThrowItem();
        }

        public virtual void DestroyItem()
        {
            onDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}