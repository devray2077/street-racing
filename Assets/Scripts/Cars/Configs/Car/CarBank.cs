
using UnityEngine;
using DataBanks;

namespace StreetRacing.Cars
{
    [CreateAssetMenu(fileName = nameof(CarBank), menuName = Constants.BanksRoot + nameof(CarBank))]
    public class CarBank : DataBank<CarBank, CarConfig>
    {
    }
}
