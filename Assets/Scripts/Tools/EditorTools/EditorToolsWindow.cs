
#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    public class EditorToolsWindow : EditorWindow
    {
        private string DataPrefsKey => "EditorTools_" + Application.identifier;

        [MenuItem("Tools/Editor Tools", priority = 0)]
        public static void Open()
        {
            var window = GetWindow<EditorToolsWindow>();
            window.titleContent = new GUIContent("Editor Tools");
        }

        private void OnEnable()
        {
            LoadData();
        }

        private void OnGUI()
        {
            var newTimeScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, 0f, 3f);
            if (newTimeScale != Time.timeScale)
            {
                Time.timeScale = newTimeScale;
                SaveData();
            }
        }

        private void LoadData()
        {
            var jsonData = EditorPrefs.GetString(DataPrefsKey);
            if (string.IsNullOrEmpty(jsonData))
            {
                return;
            }

            var data = JsonUtility.FromJson<EditorToolsEditorData>(jsonData);
            if (data == null)
            {
                return;
            }

            Time.timeScale = data.TimeScale;
        }

        private void SaveData()
        {
            var data = new EditorToolsEditorData();
            data.TimeScale = Time.timeScale;
            var jsonData = JsonUtility.ToJson(data);
            EditorPrefs.SetString(DataPrefsKey, jsonData);
        }
    }
}

#endif
