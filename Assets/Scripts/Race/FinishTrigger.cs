
using UnityEngine;
using StreetRacing.Cars.Player;
using StreetRacing.Cameras;

namespace StreetRacing.Race
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private RaceCameraController cameraController;
        [SerializeField] private GameObject[] environmentObjects;
        [SerializeField] private GameObject backgroundObject;
        [SerializeField] private GameObject finishScreen;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerCar>())
            {
                foreach (var environmentObject in environmentObjects)
                {
                    environmentObject.SetActive(false);
                }

                backgroundObject.SetActive(true);
                finishScreen.SetActive(true);

                cameraController.AnimateFinish();
            }
        }
    }
}
