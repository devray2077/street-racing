
using UnityEngine;
using StreetRacing.Cars.Player;

namespace StreetRacing.Cameras
{
    public class CameraShaker : MonoBehaviour, IUpdatable
    {
        [SerializeField] private PlayerCar playerActor;
        [SerializeField] private float shakeSpeed;
        [SerializeField] private float shakePower;
        [SerializeField] private AnimationCurve shakePattern;

        private Quaternion startRotation;
        private Quaternion targetRotation;
        private bool isRotating;
        private float rotationTimer;

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
            UpdateShakeAnimation(deltaTime);
        }

        private void UpdateShakeAnimation(float deltaTime)
        {
            if (isRotating)
            {
                Rotate(deltaTime);
                if (CheckDestination())
                {
                    isRotating = false;
                }
            }

            if (!isRotating)
            {
                SetDirection();
                rotationTimer = 0f;
                Rotate(0f);
                isRotating = true;
            }
        }

        private void SetDirection()
        {
            var shakePower = shakePattern.Evaluate(playerActor.NormalizedSpeed) * this.shakePower;

            startRotation = transform.localRotation;

            var rotX = Random.Range(-shakePower, shakePower);
            var rotY = Random.Range(-shakePower, shakePower);
            var rotZ = Random.Range(-shakePower, shakePower);

            targetRotation = Quaternion.Euler(rotX, rotY, rotZ);
        }

        private void Rotate(float deltaTime)
        {
            rotationTimer += deltaTime;
            var lerpValue = Mathf.Clamp01(rotationTimer / shakeSpeed);
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, lerpValue);
        }

        private bool CheckDestination()
        {
            return rotationTimer >= shakeSpeed;
        }
    }
}
