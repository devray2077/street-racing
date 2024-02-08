
using UnityEngine;

namespace StreetRacing.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = Constants.LevelsRoot + nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private SceneReference scene;

        public SceneReference Scene => scene;
    }
}
