
using UnityEngine;

namespace StreetRacing.Cameras
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
