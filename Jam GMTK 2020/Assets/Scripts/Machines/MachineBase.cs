using UnityEngine;
using UnityEngine.Events;
// using System;
using System.Collections;
using UnityEngine.InputSystem;
using GMTKJAM.UI;
using GMTKJAM.Items;

namespace GMTKJAM.Machines
{
    public class MachineBase : MonoBehaviour
    {
        public StatBar stat;
        [SerializeField]
        private float breakThreshhold = 1f;
        public float brokenness = 0f;
        [SerializeField]
        private UnityEvent onRepaired;
        [SerializeField]
        private UnityEvent onBroken;

        public void Break()
        {
            //Can't break it if it's broken
            if (brokenness > breakThreshhold)
                return;

            brokenness += Random.Range(0.1f, 0.2f);
            if (brokenness > breakThreshhold)
            {
                brokenness = breakThreshhold;
                onBroken?.Invoke();
            }
        }

        public void Repair(Wrench wrench)
        {
            brokenness -= wrench.repairSpeed;

            if (brokenness <= 0)
            {
                brokenness = 0;
                onRepaired?.Invoke();
            }
        }


    }
}