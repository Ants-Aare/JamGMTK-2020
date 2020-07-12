using UnityEngine;
using GMTKJAM.Items;
using System.Collections.Generic;

namespace GMTKJAM.Machines
{
    public class Turret : ResourceMachine
    {
        public MapController map;
        public AudioSource shootSound;
        public TraumaInducer shakeFX;
        void OnTriggerEnter(Collider collider)
        {
            Ammunition ammunition = collider.GetComponent<Ammunition>();
            if (ammunition != null && ammunition.GetComponent<Interactable>().canInteract)
            {
                AddResource(ammunition);
            }
        }
        void OnTriggerExit(Collider other)
        {
            Ammunition ammunition = GetComponent<Collider>().GetComponent<Ammunition>();
            if (ammunition != null)
            {
                RemoveResource(ammunition);
            }
        }

        public void TryShootTurret()
        {
            if (UseResource())
            {
                shootSound.Play();
                StartCoroutine(shakeFX.Start());
                map.FireGun();
            }
        }
    }
}