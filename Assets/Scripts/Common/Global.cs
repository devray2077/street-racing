
using UnityEngine;
using StreetRacing.UI;
using StreetRacing.Cars;
using StreetRacing.Flow;
using StreetRacing.Levels;
using StreetRacing.Cameras;
using StreetRacing.Progress;

namespace StreetRacing
{
    public static class Global
    {
        public static GameController GameController => GameController.Instance;
        public static UIController UIController => UIController.Instance;
        public static MainCamera MainCamera => MainCamera.Instance;
        public static CoroutinesHolder CoroutinesHolder => CoroutinesHolder.Instance;
        public static PlayerProgress PlayerProgress => PlayerProgress.Instance;

        public static Camera Camera => MainCamera.Camera;

        public static CarBank CarBank => CarBank.Instance;
        public static CarSkinBank CarSkinBank => CarSkinBank.Instance;
        public static CarTireBank CarTireBank => CarTireBank.Instance;

        public static CameraParameters CameraParameters => CameraParameters.Instance;
        public static CarMovementParameters CarMovementParameters => CarMovementParameters.Instance;
        public static LevelsParameters LevelsParameters => LevelsParameters.Instance;

        public static bool IsApplicationQuitting => GameController.IsApplicationQuitting;
    }
}
