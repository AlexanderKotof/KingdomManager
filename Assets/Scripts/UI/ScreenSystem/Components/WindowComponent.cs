using UnityEngine;

namespace ScreenSystem.Components
{
    public abstract class WindowComponent : MonoBehaviour, IWindowComponent
    {
        public bool hidenByDefault = false;

        private void Start()
        {
            OnInit();

            if (hidenByDefault)
            {
                gameObject.SetActive(false);
                return;
            }

            Show();
        }

        public void Hide()
        {
            OnHide();

            gameObject.SetActive(false);
        }

        protected virtual void OnHide() { }
        protected virtual void OnShow() { }
        public void Show()
        {
            gameObject.SetActive(true);

            OnShow();
        }

        protected virtual void OnInit() { }

        protected virtual void OnDestroy() { }
    }
}