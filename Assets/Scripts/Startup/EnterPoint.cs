
using UnityEngine;
using StreetRacing.Events;
using StreetRacing.Progress;

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
            PlayerProgress.Load();

            EventsManager.OnGameInitialized.Invoke();
        }
    }
}
