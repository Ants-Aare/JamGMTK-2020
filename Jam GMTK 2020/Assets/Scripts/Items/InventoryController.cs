using UnityEngine;
using System;
using System.Collections;
using UnityEngine.InputSystem;

namespace GMTKJAM.Items
{
    public class InventoryController : MonoBehaviour
    {
        public ResourceItem currentItem;
        [Header("Values")]
        [SerializeField]
        private float throwForceStrength;
        [SerializeField]
        private float carryForceStrength;

        [SerializeField]
        private Transform forwardTransform;
        [Header("Values")]
        [SerializeField]
        private Transform carryAnchor;

        #region Equipping and using items
        public void EquipItem(ItemBase newItem)
        {

        }
        public void UseItem(InputAction.CallbackContext context)
        {
            if (currentItem == null)
                return;

            currentItem.Use(this);
        }

        #endregion

        #region ResourceItems
        public void StartCarryItem(ResourceItem newItem)
        {
            DropItem();

            currentItem = newItem;

            StartCoroutine(CarryItem());
        }
        public void DropItem()
        {
            if (currentItem == null)
                return;

            StopAllCoroutines();
            currentItem.Drop();

            currentItem = null;
        }
        public void ThrowItem()
        {
            if (currentItem == null)
                return;

            StopAllCoroutines();
            currentItem.Drop();

            currentItem.rigidBody.AddForce((forwardTransform.forward * throwForceStrength) + Vector3.up * (throwForceStrength * 0.1f));

            currentItem = null;
        }

        private IEnumerator CarryItem()
        {
            Rigidbody rigidBody = currentItem.rigidBody;

            while (true)
            {
                Vector3 force = carryAnchor.position - currentItem.transform.position;
                rigidBody.AddForce(force * carryForceStrength * Time.deltaTime, ForceMode.VelocityChange);
                yield return null;
            }
        }

        #endregion
    }
}