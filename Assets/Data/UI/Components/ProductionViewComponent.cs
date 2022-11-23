using ScreenSystem.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KM.UI.Components
{
    public class ProductionViewComponent : WindowComponent
    {
        public Image nowProducingIcon;

        public TMP_Text timeLeftText;
        public Slider productionProgress;

        public Button speedupButton;

        public void SetInfo(Sprite icon, string timeLeft, float progress)
        {
            nowProducingIcon.sprite = icon;
            timeLeftText.text = timeLeft;
            productionProgress.value = progress;
        }

        public void UpdateProgress(string timeLeft, float progress)
        {
            timeLeftText.text = timeLeft;
            productionProgress.value = progress;
        }
    }
}