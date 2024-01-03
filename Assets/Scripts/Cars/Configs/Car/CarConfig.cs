
using UnityEngine;
using DataBanks;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarConfig), menuName = Constants.CarConfigsRoot + nameof(CarConfig))]
    public class CarConfig : DataBankItem
    {
        [SerializeField] private CarBody bodyPrefab;

        public CarBody BodyPrefab => bodyPrefab;
    }
}
