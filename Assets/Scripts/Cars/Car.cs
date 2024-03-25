
using UnityEngine;
using Sirenix.OdinInspector;
using Dreamteck.Splines;

namespace StreetRacing.Cars
{
    public class Car : MonoBehaviour, IUpdatable
    {
        [SerializeField] private CarVisualConfigurator visualConfigurator;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private SplineComputer spline;
        [SerializeField] private float startNormalizedPosition;

        private float initialXPosition;
        private float speed;
        private float normalizedDistance;

        public float CurrentSpeed => speed;
        public virtual float MaxSpeed => Global.CarMovementParameters.MaxSpeed;
        public float NormalizedSpeed => CurrentSpeed / MaxSpeed;
        [ShowInInspector]
        protected float NormalizedDistance => normalizedDistance;
        protected virtual float Acceleration => Global.CarMovementParameters.Acceleration;

        private void Awake()
        {
            initialXPosition = transform.position.x;
        }

        protected virtual void OnEnable()
        {
            UpdateSystem.AddToUpdate(this, UpdateLayer.FixedUpdate);
        }

        protected virtual void OnDisable()
        {
            UpdateSystem.RemoveFromUpdate(this, UpdateLayer.FixedUpdate);
        }

        public virtual void UpdateObject(float deltaTime)
        {
            var sample = spline.Project(transform.position);
            normalizedDistance = 1f - (float)sample.percent;
            var roadDirection = -sample.forward;
            transform.rotation = Quaternion.LookRotation(roadDirection);
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            speed = Mathf.Clamp(speed + Acceleration * Time.deltaTime, 0, MaxSpeed);

            var force = transform.forward * speed;
            var pos = transform.position;
            pos += force;
            transform.position = pos;
            //rigidbody.AddForce(force, ForceMode.Acceleration);

            //var position = transform.position;
            //position.x = initialXPosition;
            //transform.position = position;
        }
    }
}
