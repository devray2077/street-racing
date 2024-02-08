
using UnityEngine;

namespace StreetRacing.Cars
{
    public class CarVisualConfigurator : MonoBehaviour
    {
        public void SetBody(int carId)
        {
            var carBodyPrefab = Global.CarBank.GetItem(carId);
        }

        public void SetSkin(int skinId)
        {
            var carSkinPrefab = Global.CarSkinBank.GetItem(skinId);
        }

        public void SetTire(int tireId)
        {
            var carTirePrefab = Global.CarTireBank.GetItem(tireId);
        }
    }
}
