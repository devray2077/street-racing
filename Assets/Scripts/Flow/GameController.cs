
using UnityEngine;
using StreetRacing.Events;
using StreetRacing.Scenes;

namespace StreetRacing.Flow
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private SceneReference garageScene;

        public static GameController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            EventsManager.OnGameInitialized.AddListener(OnGameInitialized);
        }

        private void OnGameInitialized()
        {
            GoToGarage();
        }

        private void GoToGarage()
        {
            SceneLoader.LoadScene(garageScene, OnGarageSceneLoaded);
        }

        private void OnGarageSceneLoaded()
        {
            int c = 0;
            c++;
        }
    }
}
