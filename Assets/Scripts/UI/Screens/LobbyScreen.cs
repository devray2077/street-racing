
using System;
using UnityEngine;
using UnityEngine.UI;

namespace StreetRacing.UI
{
    public class LobbyScreen : UIScreenBase
    {
        public class Parameters : UIScreenParameters
        {
            public Action Start;
        }

        [SerializeField] private Button playButton;

        [SerializeField] private Button carsButton;
        [SerializeField] private Button tuningButton;

        private Parameters parameters;

        protected override void Initialize()
        {
            base.Initialize();

            playButton.onClick.AddListener(OnPlayButtonClicked);

            carsButton.onClick.AddListener(OnCarsButtonClicked);
            tuningButton.onClick.AddListener(OnTuningButtonClicked);
        }

        public override void Show(UIScreenParameters parameters)
        {
            base.Show(parameters);

            this.parameters = parameters as Parameters;
        }

        private void OnPlayButtonClicked()
        {
            parameters?.Start?.Invoke();
            Hide();
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
