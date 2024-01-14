
using UnityEngine;
using StreetRacing.UI;
using StreetRacing.Flow;
using StreetRacing.Cameras;

namespace StreetRacing
{
    public static class Global
    {
        public static GameController GameController => GameController.Instance;
        public static UIController UIController => UIController.Instance;
        public static MainCamera MainCamera => MainCamera.Instance;
        public static CoroutinesHolder CoroutinesHolder => CoroutinesHolder.Instance;

        public static Camera Camera => MainCamera.Camera;

        public static bool IsApplicationQuitting => GameController.IsApplicationQuitting;
    }
}
