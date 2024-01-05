
using UnityEngine;

namespace StreetRacing
{
    public class CoroutinesHolder : MonoBehaviour
    {
        public static CoroutinesHolder Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
