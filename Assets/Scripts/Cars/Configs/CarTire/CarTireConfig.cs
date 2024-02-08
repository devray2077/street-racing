
using UnityEngine;
using StreetRacing.UnlockableItems;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarTireConfig), menuName = Constants.CarConfigsRoot + nameof(CarTireConfig))]
    public class CarTireConfig : UnlockableItemConfig
    {
        [SerializeField] private GameObject prefab;

        public GameObject Prefab => prefab;
    }
}
