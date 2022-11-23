using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenSystem.Screens
{
    public abstract class BaseScreen : MonoBehaviour, IBaseScreen
    {
        public bool hidenByDefault;

        private void Start()
        {
            OnInit();

            if (!hidenByDefault)
                ShowHide(true);
            else
                gameObject.SetActive(false);
        }

        public void Show()
        {
            if (gameObject.activeSelf)
                return;

            ShowHide(true);


        }

        private void ShowHide(bool value)
        {
            gameObject.SetActive(value);
            if (value)
            {
                OnShow();
            }
            else
            {
                OnHide();
            }
        }

        public void Hide()
        {
            if (!gameObject.activeSelf)
                return;

            ShowHide(false);

        }

        protected virtual void OnInit() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

    }
}