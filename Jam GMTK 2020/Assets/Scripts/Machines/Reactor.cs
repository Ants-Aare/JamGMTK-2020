using UnityEngine;
using GMTKJAM.Items;
using System.Collections.Generic;

namespace GMTKJAM.Machines
{
    public class Reactor : ResourceMachine
    {
        [SerializeField]
        private int maxFuelAmount = 1000;
        private int fuelAmount = 0;

        void Start()
        {
            stat.maxValue = (float)maxFuelAmount;
        }
        void OnTriggerEnter(Collider collider)
        {
            ReactorFuel reactorFuel = collider.GetComponent<ReactorFuel>();
            if (reactorFuel != null)
            {
                AddResource(reactorFuel);
            }
        }

        protected override void AddResource(ResourceItem resource)
        {
            if (fuelAmount < maxFuelAmount)
            {
                onResourceAdded?.Invoke();
                fuelAmount += resource.resourceAmount;
                resource.DestroyItem();
                UpdateStat();
            }
        }

        public override bool UseResource()
        {
            if (fuelAmount <= 0)
            {
                onResourceEmpty?.Invoke();
                return false;
            }
            else
            {
                fuelAmount--;

                UpdateStat();
                return true;
            }
        }

        public override void UpdateStat()
        {
            stat.UpdateFill((float)fuelAmount);
        }
    }
}