
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DataBanks
{
    public class DataBankItemsCollector
    {
        private static List<Object> delayedDataBankForUpdate = new List<Object>(); // Object - DataBank

        private class AssetCreationDetector : AssetPostprocessor
        {
            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
            {
                foreach (var assetPath in importedAssets)
                {
                    OnAssetCreated(assetPath);
                }

                OnAssetsDeleted();
            }
        }

        private class AssetDeletionDetector : AssetModificationProcessor
        {
            private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
            {
                OnBeforeAssetDeletion(assetPath);
                return AssetDeleteResult.DidNotDelete;
            }
        }

        private static void OnAssetCreated(string assetPath)
        {
            var dataBank = GetDataBankByAssetPath(assetPath);
            if (dataBank == null)
            {
                return;
            }

            if (TryCollectDataBankItems(dataBank))
            {
                AssetDatabase.SaveAssets();
            }
        }

        private static void OnBeforeAssetDeletion(string assetPath)
        {
            var dataBank = GetDataBankByAssetPath(assetPath);
            if (dataBank == null)
            {
                return;
            }

            if (delayedDataBankForUpdate.Contains(dataBank))
            {
                return;
            }

            delayedDataBankForUpdate.Add(dataBank);
        }

        private static void OnAssetsDeleted()
        {
            var isAnyDataBankIsUpdated = false;

            foreach (var dataBank in delayedDataBankForUpdate)
            {
                isAnyDataBankIsUpdated |= TryCollectDataBankItems(dataBank);
            }
            delayedDataBankForUpdate.Clear();

            if (isAnyDataBankIsUpdated)
            {
                AssetDatabase.SaveAssets();
            }
        }

        private static Object GetDataBankByAssetPath(string assetPath)
        {
            var assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            if (assetType == null || !assetType.IsSubclassOf(typeof(DataBankItem)))
            {
                return null;
            }
        
            var asset = AssetDatabase.LoadAssetAtPath<DataBankItem>(assetPath);
            if (asset == null)
            {
                return null;
            }

            return GetDataBankByItem(asset);
        }
        
        private static Object GetDataBankByItem(DataBankItem dataBankItem)
        {
            var dataBankItemType = dataBankItem.GetType();
        
            var loadedDataBanks = Resources.LoadAll("DataBanks");
            foreach (var dataBank in loadedDataBanks)
            {
                var dataBankType = dataBank.GetType();
        
                var itemsProperty = dataBankType.GetProperty("Items", BindingFlags.Instance | BindingFlags.Public);
                var itemType = itemsProperty.PropertyType.GetElementType();
        
                if (dataBankItemType == itemType)
                {
                    return dataBank;
                }
            }
        
            return null;
        }
        
        private static bool TryCollectDataBankItems(Object dataBank)
        {
            var dataBankType = dataBank.GetType();
            var collectItemsMethod = dataBankType.GetMethod("CollectItems", BindingFlags.NonPublic | BindingFlags.Instance);
            collectItemsMethod.Invoke(dataBank, null);
        
            return false;
        }
    }
}
