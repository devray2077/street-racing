
using UnityEngine;

namespace StreetRacing.Cameras
{
    [CreateAssetMenu(fileName = nameof(CameraParameters), menuName = Constants.ParametersRoot + nameof(CameraParameters))]
    public class CameraParameters : StaticScriptableObject<CameraParameters>
    {
        [SerializeField] private CameraStateInfo raceMinSpeedCameraState;
        [SerializeField] private CameraStateInfo raceMaxSpeedCameraState;
        [SerializeField] private float raceInterpolationSpeedBetweenStates;

        public CameraStateInfo RaceMinSpeedCameraState => raceMinSpeedCameraState;
        public CameraStateInfo RaceMaxSpeedCameraState => raceMaxSpeedCameraState;
        public float RaceInterpolationSpeedBetweenStates => raceInterpolationSpeedBetweenStates;
    }
}
