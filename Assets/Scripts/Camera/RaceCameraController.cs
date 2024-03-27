
using System.Collections;
using UnityEngine;
using DG.Tweening;
using StreetRacing.Cars.Player;
using StreetRacing.Events;

namespace StreetRacing.Cameras
{
    public class RaceCameraController : MonoBehaviour, IUpdatable
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private PlayerCar target;
        [SerializeField] private Transform finishPivot;
        [SerializeField] private float finishFOV;
        [SerializeField] private AnimationCurve finishTimeScaleCurve;
        [SerializeField] private float timeScaleAnimationDuration;
        [SerializeField] private Transform startPivot1;
        [SerializeField] private Transform startPivot1End;
        [SerializeField] private Transform startPivot2;
        [SerializeField] private Transform startPivot2End;
        [SerializeField] private Transform startPivot3;
        [SerializeField] private float startPivot1Duration;
        [SerializeField] private float startPivot2Duration;
        [SerializeField] private float startPivot3Duration;

        private CameraParameters cameraParameters;
        private float currentNormalizedCarSpeed;
        private bool isFinish;
        private bool isRaceStarted;

        private void Awake()
        {
            cameraParameters = Global.CameraParameters;
            StartRaceAnimation();
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

        public void AnimateFinish()
        {
            isFinish = true;

            camera.fieldOfView = finishFOV;
            transform.position = finishPivot.position;
            transform.rotation = finishPivot.rotation;

            var cameraShaker = GetComponentInChildren<CameraShaker>();
            if (cameraShaker != null)
            {
                cameraShaker.enabled = false;
            }

            StartCoroutine(AnimateTimeScale());
        }

        private void UpdatePosition(float deltaTime)
        {
            if (isFinish || !isRaceStarted)
            {
                return;
            }

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

        private IEnumerator AnimateTimeScale()
        {
            for (var i = 0f; i <= timeScaleAnimationDuration; i += Time.unscaledDeltaTime)
            {
                var lerpValue = i / timeScaleAnimationDuration;
                Time.timeScale = finishTimeScaleCurve.Evaluate(lerpValue);

                yield return null;
            }

            Time.timeScale = finishTimeScaleCurve.Evaluate(1f);
        }

        private void StartRaceAnimation()
        {
            transform.position = startPivot1.position;
            transform.rotation = startPivot1.rotation;

            transform.DOMove(startPivot1End.position, startPivot1Duration).SetEase(Ease.Linear).OnComplete(StartPivot2);
            transform.DORotateQuaternion(startPivot1End.rotation, startPivot1Duration).SetEase(Ease.Linear);

            void StartPivot2()
            {
                transform.DOKill(true);

                transform.position = startPivot2.position;
                transform.rotation = startPivot2.rotation;

                transform.DOMove(startPivot2End.position, startPivot2Duration).SetEase(Ease.Linear).OnComplete(StartPivot3);
                transform.DORotateQuaternion(startPivot2End.rotation, startPivot2Duration).SetEase(Ease.Linear);
            }

            void StartPivot3()
            {
                transform.DOKill(true);

                transform.position = startPivot3.position;
                transform.rotation = startPivot3.rotation;

                var raceMinSpeedCameraState = cameraParameters.RaceMinSpeedCameraState;

                var carTRS = target.transform.localToWorldMatrix;
                var localPoint = raceMinSpeedCameraState.offset;
                var worldPoint = carTRS.MultiplyPoint3x4(localPoint);
                var localRotation = raceMinSpeedCameraState.rotation;
                var worldRotation = localRotation * carTRS.rotation;
                var worldRotationEuler = worldRotation.eulerAngles;
                worldRotationEuler.z = 0f;
                worldRotation = Quaternion.Euler(worldRotationEuler);

                var targetPosition = worldPoint;
                var targetRotation = worldRotation;

                transform.DOMove(targetPosition, startPivot3Duration).SetEase(Ease.Linear).OnComplete(StartRace);
                transform.DORotateQuaternion(targetRotation, startPivot3Duration).SetEase(Ease.Linear);
                camera.DOFieldOfView(raceMinSpeedCameraState.fov, startPivot3Duration);
            }

            void StartRace()
            {
                transform.DOKill(true);

                isRaceStarted = true;
                EventsManager.RaceStarted.Invoke();
            }
        }
    }
}
