
using UnityEngine;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarSkinConfig), menuName = Constants.CarConfigsRoot + nameof(CarSkinConfig))]
    public class CarSkinConfig : ScriptableObject
    {
        [SerializeField] private Material material;

        public Material Material => material;
    }
}
