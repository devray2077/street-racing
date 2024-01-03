
using UnityEngine;
using Sirenix.OdinInspector;

namespace DataBanks
{
    [InlineProperty]
    public abstract class DataBankItemID<TBank, TItem>
        where TItem : DataBankItem
        where TBank : DataBank<TBank, TItem>
    {
        [SerializeField, HideLabel, ValueDropdown(nameof(AvailableItemIDsForSelection))]
        private int id;

        public int ID => id;
        public bool IsValid => ID > 0;

        private DataBank<TBank, TItem> DataBank => DataBank<TBank, TItem>.Instance;

        private TItem item;
        public TItem Item
        {
            get
            {
                if (item == null)
                {
                    item = DataBank.GetItem(id);
                }
                return item;
            }
        }

        private ValueDropdownList<int> AvailableItemIDsForSelection
        {
            get
            {
                var availableItems = DataBank.Items;

                var result = new ValueDropdownList<int>();
                result.Add($"None", 0);
                foreach (var item in availableItems)
                {
                    result.Add($"[{item.ID}] ({item.name})", item.ID);
                }
                return result;
            }
        }
    }
}
