using UnityEngine;
using GMTKJAM.Items;
using System.Collections.Generic;

namespace GMTKJAM.Machines
{
    public class Turret : ResourceMachine
    {
        void OnTriggerEnter(Collider collider)
        {
            Ammunition ammunition = collider.GetComponent<Ammunition>();
            if (ammunition != null)
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
    }
}