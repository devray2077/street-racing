
using UnityEngine;

namespace StreetRacing.Cameras
{
    public class CameraHolder : MonoBehaviour
    {
        [SerializeField] private Camera previewCamera;

        private void Awake()
        {
            if (previewCamera != null)
            {
                previewCamera.enabled = false;
            }

            Global.MainCamera.AttachToHolder(this);
        }

        private void OnDestroy()
        {
            if (Global.IsApplicationQuitting)
            {
                return;
            }

            Global.MainCamera.DettachFromHolder(this);
        }
    }
}
