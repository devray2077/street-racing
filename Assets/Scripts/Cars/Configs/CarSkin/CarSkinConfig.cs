
using UnityEngine;
using StreetRacing.UnlockableItems;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarSkinConfig), menuName = Constants.CarConfigsRoot + nameof(CarSkinConfig))]
    public class CarSkinConfig : UnlockableItemConfig
    {
        [SerializeField] private Material material;

        public Material Material => material;
    }
}
