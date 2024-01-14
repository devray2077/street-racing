
using UnityEngine;
using UnityEngine.UI;

namespace StreetRacing.UI
{
    public class LobbyScreen : UIScreenBase
    {
        [SerializeField] private Button playButton;

        [SerializeField] private Button carsButton;
        [SerializeField] private Button tuningButton;

        protected override void Initialize()
        {
            base.Initialize();

            playButton.onClick.AddListener(OnPlayButtonClicked);

            carsButton.onClick.AddListener(OnCarsButtonClicked);
            tuningButton.onClick.AddListener(OnTuningButtonClicked);
        }

        private void OnPlayButtonClicked()
        {

        }

        private void OnCarsButtonClicked()
        {
            Global.UIController.ChangeScreen<CarsScreen>();
        }

        private void OnTuningButtonClicked()
        {
            Global.UIController.ChangeScreen<TuningScreen>();
        }
    }
}
