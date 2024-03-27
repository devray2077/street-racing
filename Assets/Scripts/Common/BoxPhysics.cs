
using UnityEngine;
using StreetRacing.Cars;

namespace StreetRacing
{
    public class BoxPhysics : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float force;

        private bool isFirstCollision = true;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.GetComponent<Car>())
            {
                return;
            }

            if (!isFirstCollision)
            {
                return;
            }

            isFirstCollision = false;

            rigidbody.AddForce(new Vector3(0f, 0.25f, 1f) * force);
        }
    }
}
