
#if UNITY_EDITOR

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FavoriteAssets
{
    public class FavoriteAssetsWindow : EditorWindow
    {
        private List<Object> objectsList;
        private Vector2 scrollPosition = Vector2.zero;
        
        private string DataPrefsKey => "FavoriteAssets_" + Application.identifier;

        [MenuItem("Tools/Favorite Assets", priority = 0)]
        public static void Open()
        {
            var window = GetWindow<FavoriteAssetsWindow>();
            window.titleContent = new GUIContent("Favorite Assets");
        }

        private void OnEnable()
        {
            LoadData();
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            var objectToRemove = default(Object);

            var objectIndexToMove = -1;
            var moveDirection = 0;

            for (var i = 0; i < objectsList.Count; i++)
            {
                DrawObjectField(objectsList[i],
                    (newObjectReference) => objectsList[i] = newObjectReference,
                    () => objectToRemove = objectsList[i],
                    (direction) =>
                    {
                        objectIndexToMove = i;
                        moveDirection = direction;
                    });
            }

            if (objectToRemove != null)
            {
                objectsList.Remove(objectToRemove);
                SaveData();
            }

            if (objectIndexToMove != -1)
            {
                var objectIndexForReplace = objectIndexToMove + moveDirection;
                if (objectIndexForReplace < 0)
                {
                    objectIndexForReplace = objectsList.Count - 1;
                }
                else if (objectIndexForReplace > objectsList.Count - 1)
                {
                    objectIndexForReplace = 0;
                }

                var temp = objectsList[objectIndexToMove];
                objectsList[objectIndexToMove] = objectsList[objectIndexForReplace];
                objectsList[objectIndexForReplace] = temp;

                SaveData();
            }

            DrawObjectField(null, (newObjectReference) => objectsList.Add(newObjectReference));

            GUILayout.EndScrollView();
        }

        private void LoadData()
        {
            var jsonData = EditorPrefs.GetString(DataPrefsKey);
            if (string.IsNullOrEmpty(jsonData))
            {
                objectsList = new List<Object>();
                return;
            }

            var favoriteAssetsData = JsonUtility.FromJson<FavoriteAssetsEditorData>(jsonData);
            if (favoriteAssetsData == null)
            {
                objectsList = new List<Object>();
                return;
            }

            objectsList = favoriteAssetsData.AssetsPaths.Select(x => AssetDatabase.LoadAssetAtPath<Object>(x)).ToList();
        }

        private void SaveData()
        {
            var favoriteAssetsData = new FavoriteAssetsEditorData();
            favoriteAssetsData.AssetsPaths = objectsList.Select(x => AssetDatabase.GetAssetPath(x)).ToArray();
            var jsonData = JsonUtility.ToJson(favoriteAssetsData);
            EditorPrefs.SetString(DataPrefsKey, jsonData);
        }

        private void DrawObjectField(Object @object, System.Action<Object> onChanged = null, System.Action onRemove = null, System.Action<int> onMoved = null)
        {
            GUILayout.BeginHorizontal();

            var newObjectReference = EditorGUILayout.ObjectField(@object, typeof(Object), true);
            if (newObjectReference != @object)
            {
                if (newObjectReference == null)
                {
                    onRemove?.Invoke();
                }
                else
                {
                    onChanged?.Invoke(newObjectReference);
                    SaveData();
                }
            }

            if (@object != null)
            {
                if (GUILayout.Button("↑", EditorStyles.miniButtonMid, GUILayout.Width(20f)))
                {
                    onMoved?.Invoke(-1);
                }

                if (GUILayout.Button("↓", EditorStyles.miniButtonMid, GUILayout.Width(20f)))
                {
                    onMoved?.Invoke(1);
                }

                if (GUILayout.Button("X", EditorStyles.miniButtonMid, GUILayout.Width(20f)))
                {
                    onRemove?.Invoke();
                }

                if (GUILayout.Button("⦿", EditorStyles.miniButtonMid, GUILayout.Width(20f)))
                {
                    AssetDatabase.OpenAsset(@object);
                }
            }

            GUILayout.EndHorizontal();
        }
    }
}

#endif
