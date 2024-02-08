
using UnityEngine;
using StreetRacing.UnlockableItems;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarConfig), menuName = Constants.CarConfigsRoot + nameof(CarConfig))]
    public class CarConfig : UnlockableItemConfig
    {
        [SerializeField] private CarBody bodyPrefab;

        public CarBody BodyPrefab => bodyPrefab;
    }
}
