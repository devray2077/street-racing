
using UnityEngine;
using UnityEngine.UI;

namespace StreetRacing.UI
{
    public class CarsScreen : UIScreenBase
    {
        [SerializeField] private Button backButton;

        protected override void Initialize()
        {
            base.Initialize();

            backButton.onClick.AddListener(Hide);
        }
    }
}
