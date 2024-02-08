
using UnityEngine;

namespace StreetRacing.Cars
{
    public class Car : MonoBehaviour, IUpdatable
    {
        [SerializeField] private CarVisualConfigurator visualConfigurator;
        [SerializeField] private new Rigidbody rigidbody;

        public float CurrentSpeed => rigidbody.velocity.magnitude;
        public float MaxSpeed => Global.CarMovementParameters.MaxSpeed;

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
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            if (CurrentSpeed >= MaxSpeed)
            {
                return;
            }

            var acceleration = Global.CarMovementParameters.Acceleration;
            var force = Vector3.forward * acceleration;
            rigidbody.AddForce(force, ForceMode.Acceleration);

            var position = transform.position;
            position.x = 0f;
            transform.position = position;
        }
    }
}
