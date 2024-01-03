
using UnityEngine;
using DataBanks;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarTireConfig), menuName = Constants.CarConfigsRoot + nameof(CarTireConfig))]
    public class CarTireConfig : DataBankItem
    {
        [SerializeField] private GameObject prefab;

        public GameObject Prefab => prefab;
    }
}
