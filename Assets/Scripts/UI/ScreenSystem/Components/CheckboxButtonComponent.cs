using System;
using UnityEngine.UI;

namespace ScreenSystem.Components
{
    public class CheckboxButtonComponent : ButtonComponent
    {
        public Image checkedImage;
        public Image uncheckedImage;

        public bool canBeUnchecked = true;

        event Action<bool> OnStateChanged;

        private bool _checkedState;

        private void Start()
        {
            SetCallback(() =>
            {

                SetCheckedState(canBeUnchecked ? !_checkedState : true);
            });
        }

        public void SetCheckedState(bool state, bool callEvent = true)
        {
            _checkedState = state;

            checkedImage.gameObject.SetActive(state);
            uncheckedImage.gameObject.SetActive(!state);

            if (callEvent)
                OnStateChanged?.Invoke(state);
        }

        public void SetCallback(Action<bool> callback)
        {
            OnStateChanged += callback;
        }

        public void RemoveCallbacks()
        {
            OnStateChanged = null;
        }
    }
}