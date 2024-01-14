
using System;
using UnityEngine;

namespace StreetRacing.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RectTransform screensRoot;

        public static UIController Instance { get; private set; }

        private UIScreenBase currentScreen;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowScreen<T>() where T : UIScreenBase
        {
            ShowScreen<T>(null, null);
        }

        public void ShowScreen<T>(UIScreenParameters parameters) where T : UIScreenBase
        {
            ShowScreen<T>(parameters);
        }

        public void ShowScreen<T>(Action onShownCallback) where T : UIScreenBase
        {
            ShowScreen<T>(null, onShownCallback);
        }

        public void ShowScreen<T>(UIScreenParameters parameters, Action onShownCallback) where T : UIScreenBase
        {
            var screen = GetScreen<T>();
            if (screen != null)
            {
                onShownCallback?.Invoke();
                return;
            }

            var screenPrefab = Resources.Load<T>("Screens/" + typeof(T).Name);
            var screenObject = Instantiate(screenPrefab, screensRoot);

            currentScreen = screenObject;

            if (onShownCallback != null)
            {
                screenObject.OnShown += onShownCallback;
            }

            screenObject.Show(parameters);
        }

        public void ChangeScreen<T>() where T : UIScreenBase
        {
            ChangeScreen<T>(null, null);
        }

        public void ChangeScreen<T>(UIScreenParameters parameters) where T : UIScreenBase
        {
            ChangeScreen<T>(parameters);
        }

        public void ChangeScreen<T>(Action onShownCallback) where T : UIScreenBase
        {
            ChangeScreen<T>(null, onShownCallback);
        }

        public void ChangeScreen<T>(UIScreenParameters parameters, Action onShownCallback) where T : UIScreenBase
        {
            if (currentScreen == null)
            {
                ShowNextScreen();
            }
            else
            {
                currentScreen.OnHidden += ShowNextScreen;
                currentScreen.Hide();
            }

            void ShowNextScreen()
            {
                ShowScreen<T>(parameters, onShownCallback);
            }
        }

        public void HideScreen<T>(Action onHiddenCallback = null) where T : UIScreenBase
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

            currentScreen = null;
        }

        public T GetScreen<T>() where T : UIScreenBase
        {
            return GetComponentInChildren<T>();
        }
    }
}