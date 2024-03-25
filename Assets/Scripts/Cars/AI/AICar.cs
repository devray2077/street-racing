
using UnityEngine;

namespace StreetRacing.Cars.Player
{
    public class AICar : Car
    {
        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private float maxSpeedCoefficient;

        public override float MaxSpeed => base.MaxSpeed * maxSpeedCoefficient;
        protected override float Acceleration => accelerationCurve.Evaluate(NormalizedDistance) * base.Acceleration;
    }
}
