
using UnityEngine;
using DataBanks;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarSkinBank), menuName = Constants.BanksRoot + nameof(CarSkinBank))]
    public class CarSkinBank : DataBank<CarSkinBank, CarSkinConfig>
    {
    }
}
