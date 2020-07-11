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
            if (selectedTool == -1)
            {
                //Use the currently held resource
                if (currentItem == null)
                    return;

                switch (context.phase)
                {
                    case InputActionPhase.Started:
                        currentItem.Use(player);
                        break;
                    case InputActionPhase.Canceled:
                        currentItem.StopUse();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //Use the selected tool
            }
        }

        #endregion

        #region Tools
        public void EquipItem(ToolItem newItem)
        {
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
                rigidBody.AddForce(force * carryForceStrength * Time.deltaTime, ForceMode.VelocityChange);
                yield return null;
            }
        }

        #endregion
    }
}