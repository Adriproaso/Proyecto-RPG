using UnityEngine;

namespace RPG.Inventories
{
    [RequireComponent(typeof(Pickup))]
    public class RunOverPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponent<Pickup>().PickupItem();
            }
        }
    }
}
