
using UnityEngine;

namespace StreetRacing.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RectTransform screensRoot;

        public static UIController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ShowScreen<T>(UIScreenParameters parameters = null, System.Action onShownCallback = null) where T : UIScreenBase
        {
            var screen = GetScreen<T>();
            if (screen != null)
            {
                onShownCallback?.Invoke();
                return;
            }

            var screenPrefab = Resources.Load<T>("Screens/" + typeof(T).Name);
            var screenObject = Instantiate(screenPrefab, screensRoot);

            if (onShownCallback != null)
            {
                screenObject.OnShown += onShownCallback;
            }

            screenObject.Show(parameters);
        }

        public void HideScreen<T>(System.Action onHiddenCallback = null) where T : UIScreenBase
        {
            var screen = GetScreen<T>();
            if (screen == null)
            {
                onHiddenCallback?.Invoke();
                return;
            }

            if (onHiddenCallback != null)
            {
                screen.OnHidden += onHiddenCallback;
            }

            screen.Hide();
        }

        public T GetScreen<T>() where T : UIScreenBase
        {
            return GetComponentInChildren<T>();
        }
    }
}