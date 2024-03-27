
using UnityEngine;

namespace StreetRacing.Cameras
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private new Camera camera;

        public static MainCamera Instance { get; private set; }
        public Camera Camera => camera;

        private void Awake()
        {
            Instance = this;
        }

        public void AttachToHolder(CameraHolder cameraHolder)
        {
            transform.SetParent(cameraHolder.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public void DettachFromHolder(CameraHolder cameraHolder)
        {
            if (Global.GameController == null)
            {
                return;
            }

            if (transform.parent == cameraHolder.transform)
            {
                transform.SetParent(Global.GameController.transform);
            }
        }
    }
}
