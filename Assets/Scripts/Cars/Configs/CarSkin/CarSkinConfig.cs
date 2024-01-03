
using UnityEngine;
using DataBanks;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarSkinConfig), menuName = Constants.CarConfigsRoot + nameof(CarSkinConfig))]
    public class CarSkinConfig : DataBankItem
    {
        [SerializeField] private Material material;

        public Material Material => material;
    }
}
