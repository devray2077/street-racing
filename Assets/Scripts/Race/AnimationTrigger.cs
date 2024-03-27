
using UnityEngine;
using StreetRacing.Cars.Player;

namespace StreetRacing.Race
{
    public class AnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Animator animationController;

        private void Awake()
        {
            if (animationController == null)
            {
                Destroy(this);
                return;
            }

            animationController.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerCar>())
            {
                animationController.enabled = true;
            }
        }
    }
}
