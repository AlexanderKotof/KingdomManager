using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenSystem.Components
{
    public class ButtonComponent : WindowComponent
    {
        public Button button;
        public TMP_Text text;
        public Image image;

        private void OnValidate()
        {
            button = GetComponent<Button>();
            text = GetComponentInChildren<TMP_Text>();
        }

        public void SetText(string text)
        {
            if (!this.text)
                return;

            this.text.text = text;
        }

        public void SetImage(Sprite sprite)
        {
            if (!image)
                return;

            image.sprite = sprite;
        }

        public void SetCallback(Action callback)
        {
            button.onClick.AddListener(callback.Invoke);
        }

        public void RemoveCallback(Action callback)
        {
            button.onClick.RemoveListener(callback.Invoke);
        }

        public void RemoveAllCallbacks()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}