
using UnityEngine;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarMovementParameters), menuName = Constants.ParametersRoot + nameof(CarMovementParameters))]
    public class CarMovementParameters : StaticScriptableObject<CarMovementParameters>
    {
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;

        public float Acceleration => acceleration;
        public float MaxSpeed => maxSpeed;
    }
}
