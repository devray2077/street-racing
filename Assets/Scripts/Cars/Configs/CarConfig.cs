
using UnityEngine;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarConfig), menuName = Constants.CarConfigsRoot + nameof(CarConfig))]
    public class CarConfig : ScriptableObject
    {
        [SerializeField] private CarBody bodyPrefab;

        public CarBody BodyPrefab => bodyPrefab;
    }
}
