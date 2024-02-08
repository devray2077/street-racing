
namespace StreetRacing.UnlockableItems
{
    public enum EUnlockableItemType
    {
        Car,
        Skin,
        Tire
    }

    public static class UnlockableItemTypeExtensions
    {
        public static UnlockableItemsGameData GetUnlockableItemsData(this EUnlockableItemType type)
        {
            return type switch
            {
                EUnlockableItemType.Car => Global.PlayerProgress.CarsData,
                EUnlockableItemType.Skin => Global.PlayerProgress.CarSkinsData,
                EUnlockableItemType.Tire => Global.PlayerProgress.CarTiresData,
                _ => null,
            };
        }
    }
}
