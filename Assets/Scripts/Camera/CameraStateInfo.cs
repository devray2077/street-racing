
using System;
using UnityEngine;

namespace StreetRacing.Cameras
{
    [Serializable]
    public struct CameraStateInfo
    {
        public Vector3 offset;
        public Quaternion rotation;
        public float fov;

        public static CameraStateInfo Lerp(CameraStateInfo a, CameraStateInfo b, float t)
        {
            return new CameraStateInfo()
            {
                offset = Vector3.Lerp(a.offset, b.offset, t),
                rotation = Quaternion.Lerp(a.rotation, b.rotation, t),
                fov = Mathf.Lerp(a.fov, b.fov, t)
            };
        }
    }
}
