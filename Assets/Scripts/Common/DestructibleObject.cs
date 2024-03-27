
using UnityEngine;
using StreetRacing.Cars;

namespace StreetRacing
{
    public class DestructibleObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] rigidbodies;
        [SerializeField] private float force;

        private bool isTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponentInParent<Car>())
            {
                return;
            }

            if (isTriggered)
            {
                return;
            }

            isTriggered = true;

            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
                rb.AddForce(new Vector3(Random.Range(-0.25f, 0.25f), 0.25f, 1f) * force);
            }
        }
    }
}
