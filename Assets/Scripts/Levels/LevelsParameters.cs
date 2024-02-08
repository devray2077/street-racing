
using UnityEngine;

namespace StreetRacing.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelsParameters), menuName = Constants.ParametersRoot + nameof(LevelsParameters))]
    public class LevelsParameters : StaticScriptableObject<LevelsParameters>
    {
        [SerializeField]
        private LevelConfig[] levels;
    }
}
