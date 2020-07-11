using UnityEngine;
using GMTKJAM.Items;
using System.Collections.Generic;

namespace GMTKJAM.Machines
{
    public class Turret : MachineBase
    {
        public List<AmmoItem> storedAmmunition = new List<AmmoItem>();
        private AmmoItem currentAmmunition;

        void OnTriggerEnter(Collider collider)
        {
            AmmoItem ammo = collider.GetComponent<AmmoItem>();
            if (ammo != null)
            {
                if (!storedAmmunition.Contains(ammo))
                    storedAmmunition.Add(ammo);

                ammo.GetComponent<Interactable>().CanInteract(false);
            }
        }
        void OnTriggerExit(Collider other)
        {
            AmmoItem ammo = GetComponent<Collider>().GetComponent<AmmoItem>();
            if (ammo != null)
            {
                if (storedAmmunition.Contains(ammo))
                    storedAmmunition.Remove(ammo);

                ammo.GetComponent<Interactable>().CanInteract(false);
            }
        }

        public bool UseAmmunition()
        {
            if (currentAmmunition == null)
            {
                if (storedAmmunition.Count == 0)
                    return false;
                else
                {
                    currentAmmunition = storedAmmunition[storedAmmunition.Count - 1];
                }
            }

            currentAmmunition.resourceAmount--;

            if (currentAmmunition.resourceAmount <= 0)
            {
                storedAmmunition.Remove(currentAmmunition);
                Destroy(currentAmmunition.gameObject);
                currentAmmunition = null;
            }
            return true;
        }
    }
}