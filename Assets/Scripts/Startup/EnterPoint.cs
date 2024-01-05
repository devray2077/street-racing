
using UnityEngine;
using StreetRacing.Events;

namespace StreetRacing.Startup
{
    public class EnterPoint : MonoBehaviour
    {
        private void Start()
        {
            InitializeGame();
            Destroy(gameObject);
        }

        private void InitializeGame()
        {
            EventsManager.OnGameInitialized.Invoke();
        }
    }
}
