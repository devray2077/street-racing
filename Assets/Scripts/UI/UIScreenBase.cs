
using UnityEngine;

namespace StreetRacing.UI
{
    public abstract class UIScreenParameters
    {
    }

    public abstract class UIScreenBase : MonoBehaviour
    {
        private readonly int AnimationShowHash = Animator.StringToHash("Show");
        private readonly int AnimationHideHash = Animator.StringToHash("Hide");

        public event System.Action OnShown;
        public event System.Action OnHidden;

        private CanvasGroup canvasGroup;
        private Animator screenAnimator;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            screenAnimator = GetComponent<Animator>();

            canvasGroup.blocksRaycasts = false;
        }

        public virtual void Show(UIScreenParameters parameters)
        {
            screenAnimator.Play(AnimationShowHash);
            screenAnimator.Update(0f);
        }

        public virtual void Hide()
        {
            canvasGroup.blocksRaycasts = false;
            screenAnimator.Play(AnimationHideHash);
        }

        protected virtual void OnScreenShown()
        {
            canvasGroup.blocksRaycasts = true;
            OnShown?.Invoke();
        }

        protected virtual void OnScreenHidden()
        {
            OnHidden?.Invoke();
            Destroy(gameObject);
        }
    }
}