
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DataBanks
{
    public abstract class DataBank<TBank, TItem> : ScriptableObject
        where TBank : ScriptableObject
        where TItem : DataBankItem
    {
        [SerializeField] private TItem[] items;

        public TItem[] Items => items;

        public TItem GetItem(int id) => items.FirstOrDefault(x => x.ID == id);

        private static TBank instance;
        public static TBank Instance
        {
            get
            {
                if (instance == null)
                {
                    var path = $"DataBanks/{typeof(TBank).Name}";

#if UNITY_EDITOR
                    if (Application.isPlaying)
                    {
                        instance = Resources.Load(path) as TBank;
                    }
                    else
                    {
                        path = $"Assets/Resources/{path}.asset";
                        instance = AssetDatabase.LoadAssetAtPath<TBank>(path);
                    }
#else
                    instance = Resources.Load(path) as Bank;
#endif
                }

                return instance;
            }
        }

#if UNITY_EDITOR
        [Button]
        protected void CollectItems()
        {
            var itemType = typeof(TItem);

            items = AssetDatabase.FindAssets("t:" + itemType.Name)
                .Select(x => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(x), itemType) as TItem)
                .Where(x => x != null)
                .OrderBy(x => x.ID)
                .ToArray();

            EditorUtility.SetDirty(this);
        }
#endif
    }
}
