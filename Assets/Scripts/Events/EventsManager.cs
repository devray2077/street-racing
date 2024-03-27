
using UnityEngine.Events;

namespace StreetRacing.Events
{
    public static class EventsManager
    {
        public static UnityEvent OnGameInitialized = new UnityEvent();
        public static UnityEvent RaceStarted = new UnityEvent();
    }
}
