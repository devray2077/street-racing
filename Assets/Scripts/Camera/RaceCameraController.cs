
using UnityEngine;
using StreetRacing.Cars.Player;

namespace StreetRacing.Cameras
{
    public class RaceCameraController : MonoBehaviour, IUpdatable
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private PlayerCar target;

        private CameraParameters cameraParameters;
        private float currentNormalizedCarSpeed;

        private void Awake()
        {
            cameraParameters = Global.CameraParameters;
        }

        private void OnEnable()
        {
            UpdateSystem.AddToUpdate(this, UpdateLayer.LateUpdate);
        }

        private void OnDisable()
        {
            UpdateSystem.RemoveFromUpdate(this, UpdateLayer.LateUpdate);
        }

        public void UpdateObject(float deltaTime)
        {
            var carCurrentSpeed = target.CurrentSpeed;
            var carMaxSpeed = target.MaxSpeed;

            var raceMinSpeedCameraState = cameraParameters.RaceMinSpeedCameraState;
            var raceMaxSpeedCameraState = cameraParameters.RaceMaxSpeedCameraState;
            var raceInterpolationSpeedBetweenStates = cameraParameters.RaceInterpolationSpeedBetweenStates;

            var targetNormalizedCarSpeed = Mathf.Clamp01(carCurrentSpeed / carMaxSpeed);
            currentNormalizedCarSpeed = Mathf.Lerp(currentNormalizedCarSpeed, targetNormalizedCarSpeed, raceInterpolationSpeedBetweenStates * deltaTime);
            
            var cameraStateInfo = CameraStateInfo.Lerp(raceMinSpeedCameraState, raceMaxSpeedCameraState, currentNormalizedCarSpeed);
            var targetCameraPosition = target.transform.position + cameraStateInfo.offset;

            transform.position = targetCameraPosition;
            transform.rotation = cameraStateInfo.rotation;
            camera.fieldOfView = cameraStateInfo.fov;
        }
    }
}
