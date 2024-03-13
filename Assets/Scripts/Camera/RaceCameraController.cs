
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
            UpdatePosition(deltaTime);
        }

        private int i = 0;
        private void UpdatePosition(float deltaTime)
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

            var carTRS = target.transform.localToWorldMatrix;
            var localPoint = cameraStateInfo.offset;
            var worldPoint = carTRS.MultiplyPoint3x4(localPoint);
            var localRotation = cameraStateInfo.rotation;
            var worldRotation = localRotation * carTRS.rotation;
            var worldRotationEuler = worldRotation.eulerAngles;
            worldRotationEuler.z = 0f;
            worldRotation = Quaternion.Euler(worldRotationEuler);

            transform.position = worldPoint;
            transform.rotation = worldRotation;
            camera.fieldOfView = cameraStateInfo.fov;
        }
    }
}
