
using UnityEngine;
using DataBanks;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarTireBank), menuName = Constants.BanksRoot + nameof(CarTireBank))]
    public class CarTireBank : DataBank<CarTireBank, CarTireConfig>
    {
    }
}
