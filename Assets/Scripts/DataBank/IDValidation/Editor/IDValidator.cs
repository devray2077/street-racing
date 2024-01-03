
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IDValidation
{
    public class IDValidator : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var assetsToValidate = new List<string>();

            foreach (string assetPath in importedAssets)
            {
                var mainAssetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
                if (mainAssetType.GetCustomAttribute<ObjectWithIDAttribute>() != null)
                {
                    assetsToValidate.Add(assetPath);
                }
            }

            var isAnyAssetIsUpdated = false;
            foreach (var assetToValidate in assetsToValidate)
            {
                isAnyAssetIsUpdated |= ValidateID(assetToValidate);
            }
            if (isAnyAssetIsUpdated)
            {
                AssetDatabase.SaveAssets();
            }
        }

        private static bool ValidateID(string assetPath)
        {
            var mainAssetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);

            var mainAsset = AssetDatabase.LoadAssetAtPath(assetPath, mainAssetType);
            var allAssetsOfMainType = AssetDatabase.FindAssets($"t:{mainAssetType.Name}")
                .Select(x => AssetDatabase.GUIDToAssetPath(x))
                .Where(x => x != assetPath)
                .Select(x => AssetDatabase.LoadAssetAtPath(x, mainAssetType))
                .ToArray();

            var mainAssetID = GetAssetID(mainAsset);

            var needToGenerateNewID = false;

            if (mainAssetID == 0)
            {
                needToGenerateNewID = true;
            }

            if (!needToGenerateNewID)
            {
                foreach (var asset in allAssetsOfMainType)
                {
                    var assetID = GetAssetID(asset);
                    if (assetID == mainAssetID)
                    {
                        needToGenerateNewID = true;
                    }
                }
            }

            if (needToGenerateNewID)
            {
                var newID = allAssetsOfMainType.Length > 0 ? allAssetsOfMainType.Max(x => GetAssetID(x)) + 1 : 1;
                SetAssetID(mainAsset, newID);

                EditorUtility.SetDirty(mainAsset);

                return true;
            }

            return false;
        }

        private static int GetAssetID(Object asset)
        {
            var idFieldInfo = GetIDFieldInfo(asset);
            return idFieldInfo == null ? 0 : (int)idFieldInfo.GetValue(asset);
        }

        private static void SetAssetID(Object asset, int id)
        {
            var idFieldInfo = GetIDFieldInfo(asset);
            if (idFieldInfo == null)
            {
                return;
            }
            idFieldInfo.SetValue(asset, id);
        }

        private static FieldInfo GetIDFieldInfo(Object asset)
        {
            var fieldsInfos = asset.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var fieldInfo in fieldsInfos)
            {
                if (fieldInfo.GetCustomAttribute<IDPropertyAttribute>() != null)
                {
                    return fieldInfo;
                }
            }

            return null;
        }
    }
}
