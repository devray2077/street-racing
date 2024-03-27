
using UnityEngine;
using Sirenix.OdinInspector;
using Dreamteck.Splines;
using DG.Tweening;
using StreetRacing.Events;

namespace StreetRacing.Cars
{
    public class Car : MonoBehaviour, IUpdatable
    {
        [SerializeField] private CarVisualConfigurator visualConfigurator;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private SplineComputer spline;
        [SerializeField] private float startNormalizedPosition;
        [SerializeField] private DOTweenAnimation[] tireAnimations;

        private float initialXPosition;
        private float speed;
        private float normalizedDistance;
        private bool isRaceStarted;

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
            EventsManager.RaceStarted.AddListener(OnRaceStarted);
            UpdateSystem.AddToUpdate(this, UpdateLayer.FixedUpdate);
        }

        protected virtual void OnDisable()
        {
            EventsManager.RaceStarted.RemoveListener(OnRaceStarted);
            UpdateSystem.RemoveFromUpdate(this, UpdateLayer.FixedUpdate);
        }

        private void OnRaceStarted()
        {
            isRaceStarted = true;

            foreach (var tireAnimation in tireAnimations)
            {
                tireAnimation.DOPlay();
            }
        }

        private void Update()
        {
            var sample = spline.Project(transform.position);
            normalizedDistance = 1f - (float)sample.percent;
            var roadDirection = -sample.forward;
            transform.rotation = Quaternion.LookRotation(roadDirection);

            if (!isRaceStarted)
            {
                return;
            }

            UpdateMovement(Time.deltaTime * Time.timeScale);
        }

        public virtual void UpdateObject(float deltaTime)
        {
            
        }

        private void UpdateMovement(float deltaTime)
        {
            speed = Mathf.Clamp(speed + Acceleration * deltaTime, 0, MaxSpeed);

            var force = transform.forward * speed * deltaTime;
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
