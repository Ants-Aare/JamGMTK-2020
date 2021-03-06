using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace GMTKJAM.Items
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Inventory")]
        public ResourceItem currentItem;
        private List<ToolItem> tools = new List<ToolItem>();
        private int selectedTool = -1;

        [Header("Values")]
        [SerializeField]
        private float throwForceStrength;
        [SerializeField]
        private float carryForceStrength;


        [Header("References")]
        [SerializeField]
        private Transform forwardTransform;
        [SerializeField]
        private Transform carryAnchor;
        [SerializeField]
        private Transform toolAnchor;
        [SerializeField]
        private PlayerController player;
        #region monobehaviour
        void Update()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollWheel > 0.3f)
            {
                CycleTool(1);
            }
            else if (scrollWheel < -0.3f)
            {
                CycleTool(-1);
            }
        }

        #endregion
        #region using items
        public void UseItem(InputAction.CallbackContext context)
        {
            ItemBase item = null;


            if (selectedTool < 0)
            {
                item = currentItem;
            }
            else
            {
                item = tools[selectedTool];
            }

            if (item == null)
                return;

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    item.Use(player);
                    break;
                case InputActionPhase.Canceled:
                    item.StopUse();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Tools
        public void EquipItem(ToolItem newItem)
        {
            newItem.transform.parent = toolAnchor;
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localRotation = Quaternion.identity;

            tools.Add(newItem);

            SelectTool(tools.Count - 1);
        }
        private void CycleTool(int direction)
        {
            int newSelectedTool = selectedTool;

            if (tools.Count == 0)
            {
                newSelectedTool = -1;
            }
            else
            {
                newSelectedTool += direction;
                if (newSelectedTool >= tools.Count)
                    newSelectedTool = 0;
                if (newSelectedTool < 0)
                    newSelectedTool = tools.Count - 1;
            }

            SelectTool(newSelectedTool);
        }
        private void SelectTool(int index)
        {
            if (selectedTool >= 0)
            {
                DropItem();
                tools[selectedTool].Deselect();
            }

            selectedTool = index;

            if (selectedTool >= 0)
            {
                tools[selectedTool].Select();
            }
        }

        #endregion

        #region ResourceItems
        public void StartCarryItem(ResourceItem newItem)
        {
            selectedTool = -1;

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
                rigidBody.velocity = force * carryForceStrength;
                yield return null;
            }
        }

        #endregion
    }
}