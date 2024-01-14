
using UnityEngine;
using StreetRacing.UI;
using StreetRacing.Events;
using StreetRacing.Scenes;

namespace StreetRacing.Flow
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private SceneReference garageScene;

        public static GameController Instance { get; private set; }
        public static bool IsApplicationQuitting { get; private set; }

        private void Awake()
        {
            Instance = this;

            EventsManager.OnGameInitialized.AddListener(OnGameInitialized);
        }

        private void OnApplicationQuit()
        {
            IsApplicationQuitting = true;
        }

        private void OnGameInitialized()
        {
            GoToGarage();
        }

        private void GoToGarage()
        {
            SceneLoader.LoadScene(garageScene, OnGarageSceneLoaded, () =>
            {
                Global.UIController.ShowScreen<LobbyScreen>();
            });
        }

        private void OnGarageSceneLoaded()
        {
        }
    }
}
