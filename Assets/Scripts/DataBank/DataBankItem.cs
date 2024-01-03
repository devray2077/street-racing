
using UnityEngine;
using Sirenix.OdinInspector;
using IDValidation;

namespace DataBanks
{
    [ObjectWithID]
    public abstract class DataBankItem : ScriptableObject
    {
        [SerializeField, ReadOnly, IDProperty] protected int id;

        public int ID => id;
    }
}
