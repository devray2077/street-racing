
using UnityEngine;
using Sirenix.OdinInspector;
using DataBanks;

namespace StreetRacing.UnlockableItems
{
    public abstract class UnlockableItemConfig : DataBankItem
    {
        [PreviewField(ObjectFieldAlignment.Left)]
        [SerializeField] private Sprite icon;

        public Sprite Icon => icon;
    }
}
