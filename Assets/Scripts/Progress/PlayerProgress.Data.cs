
using UnityEngine;
using StreetRacing.UnlockableItems;

namespace StreetRacing.Progress
{
    public partial class PlayerProgress
    {
        [SerializeField] private int completedLevels;

        [SerializeField] private UnlockableItemsGameData carsData = new UnlockableItemsGameData();
        [SerializeField] private UnlockableItemsGameData carSkinsData = new UnlockableItemsGameData();
        [SerializeField] private UnlockableItemsGameData carTiresData = new UnlockableItemsGameData();

        public int CompletedLevels => completedLevels;

        public UnlockableItemsGameData CarsData => carsData;
        public UnlockableItemsGameData CarSkinsData => carSkinsData;
        public UnlockableItemsGameData CarTiresData => carTiresData;

        public void CompleteLevel()
        {
            completedLevels++;
            Save();
        }

        private void InitializeDefaults()
        {

        }
    }
}
