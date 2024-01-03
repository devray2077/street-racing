
using UnityEngine;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarTireConfig), menuName = Constants.CarConfigsRoot + nameof(CarTireConfig))]
    public class CarTireConfig : ScriptableObject
    {
        [SerializeField] private GameObject prefab;

        public GameObject Prefab => prefab;
    }
}
