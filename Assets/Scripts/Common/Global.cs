
using StreetRacing.Flow;
using StreetRacing.Cameras;

namespace StreetRacing
{
    public static class Global
    {
        public static GameController GameController => GameController.Instance;
        public static CameraController CameraController => CameraController.Instance;

        public static CoroutinesHolder CoroutinesHolder => CoroutinesHolder.Instance;
    }
}
