
using System;
using UnityEngine;

namespace StreetRacing.Progress
{
    [Serializable]
    public partial class PlayerProgress
    {
        private const string PrefsKey = "StreetRacing_Progress";

        private static PlayerProgress instance;
        public static PlayerProgress Instance => instance;

        public static void Load()
        {
            var jsonData = PlayerPrefs.GetString(PrefsKey);
            if (string.IsNullOrEmpty(jsonData))
            {
                instance = new PlayerProgress();
                instance.InitializeDefaults();
            }
            else
            {
                instance = JsonUtility.FromJson<PlayerProgress>(jsonData);
                if (instance == null)
                {
                    instance = new PlayerProgress();
                    instance.InitializeDefaults();
                }
            }
        }

        public void Save()
        {
            var jsonData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(PrefsKey, jsonData);
        }
    }
}
