using KM.Features.Population;
using KM.Systems;
using ScreenSystem.Components;
using System;
using TMPro;
using UnityEngine.UI;

namespace KM.UI.CarouselScreen.Components
{
    public class ProfessionPopulationPanelComponent : WindowComponent
    {
        public TMP_Text nameText;
        public Slider countSlider;
        public TMP_Text countText;

        public Button lessButton;
        public Button moreButton;

        private PopulationData _population;
        private PopulationSystem _populationSystem;

        protected override void OnShow()
        {
            base.OnShow();

            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
        }

        public void SetInfo(PopulationData population)
        {
            _population = population;

            SetInfo(population.type.ToString(), population.Count, population.maxCount);

            _population.PopulationChanged += (count) => SetCount(count, _population.maxCount);

            SetButtonsCallback(delta => _populationSystem.ChangePopulation(_population.type, delta));
        }

        private void SetInfo(string professionName, int count, int maxCount)
        {
            nameText.text = professionName;
            SetCount(count, maxCount);
        }

        private void SetCount(int count, int maxCount)
        {
            countText.text = $"{count} / {maxCount}";
            countSlider.maxValue = maxCount;
            countSlider.value = count;
        }

        public void SetButtonsCallback(Action<int> callback)
        {
            lessButton.onClick.AddListener(() => callback.Invoke(-1));
            moreButton.onClick.AddListener(() => callback.Invoke(1));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if(_population != null)
                _population.PopulationChanged -= (count) => SetCount(count, _population.maxCount);
        }
    }
}