using UnityEngine;
using GMTKJAM.Machines;

namespace GMTKJAM.Items
{
    public class Wrench : ToolItem
    {
        public float repairSpeed = 1f;
        [SerializeField]
        private float reach = 2f;
        [SerializeField]
        private LayerMask layerMask;
        private MachineBase repairingMachine = null;
        private Transform raySource;
        private PlayerController player;
        public override void Use(PlayerController source)
        {
            raySource = source.camera.transform;
            player = source;

            player.animator?.SetBool("useWrench", true);
        }
        public override void StopUse()
        {
            player.animator?.SetBool("useWrench", false);
        }

        // Gets triggered by animation event
        public void Repair()
        {
            if (Physics.SphereCast(raySource.position, 0.3f, raySource.forward, out RaycastHit hit, reach, layerMask))
            {
                repairingMachine = hit.collider.GetComponent<MachineBase>();
                if (repairingMachine != null)
                {
                    repairingMachine.Repair(this);
                }
            }
        }
    }
}