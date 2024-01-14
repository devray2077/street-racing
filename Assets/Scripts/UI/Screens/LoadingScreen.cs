
using UnityEngine;
using UnityEngine.UI;

namespace StreetRacing.UI
{
    public class LoadingScreen : UIScreenBase
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Sprite[] backgroundVariations;

        public override void Show(UIScreenParameters parameters)
        {
            base.Show(parameters);

            backgroundImage.sprite = backgroundVariations.GetRandom();
        }
    }
}
