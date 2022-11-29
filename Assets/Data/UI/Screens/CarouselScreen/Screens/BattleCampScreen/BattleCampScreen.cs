using KM.Features.ArmyFeature;
using KM.Features.DayChange;
using KM.Features.GameEventsFeature.Events.Bonuses;
using KM.Features.Population;
using KM.Systems;
using KM.UI.Components;
using ScreenSystem;
using ScreenSystem.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KM.UI.CarouselScreens
{
    public class BattleCampScreen : GameScreen
    {
        public ListComponent recruitUnitsList;
        public ListComponent inArmyUnitsList;

        public ProductionViewComponent recruitProgressView;
        public ListComponent productionQueue;

        public TMP_Text freePeoplesText;

        public GameObject BattleReadyUI;
        public Button StartBattleButton;
        public Button ArmyInfoButton;

        ArmySystem _armySystem;
        ArmyRecruitSystem _armyRecruitSystem;
        PopulationSystem _populationSystem;
        DayChangeSystem _dayChangeSystem;


        protected void Start()
        {
            _armySystem = GameSystems.GetSystem<ArmySystem>();
            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
            _dayChangeSystem = GameSystems.GetSystem<DayChangeSystem>();
            _armyRecruitSystem = GameSystems.GetSystem<ArmyRecruitSystem>();

            recruitUnitsList.SetItems<EntityButton>(_armyRecruitSystem.ReadyToRecruit.Count, (item, par) =>
            {
                item.SetInfo(_armyRecruitSystem.ReadyToRecruit[par.index], OnArmyClick);
            });

            inArmyUnitsList.SetItems<EntityButton>(_armySystem.freeArmy.UnitTypesCount, (item, par) =>
            {
                var unit = _armySystem.freeArmy.units[par.index].prototype;
                var count = _armySystem.freeArmy.units[par.index].count;

                item.SetInfo(unit, OnArmyClick, count);
            });

            recruitProgressView.speedupButton.onClick.AddListener(_armyRecruitSystem.SpeedUp);
            recruitProgressView.Hide();

            freePeoplesText.text = _populationSystem.FreePeoples.ToString();

            _armyRecruitSystem.StartTraining += OnTrainingStarts;
            _armyRecruitSystem.TrainingProgress += OnTrainingProgress;
            _armyRecruitSystem.UnitTrained += OnTrained;

            _armyRecruitSystem.QueueUpdated += OnQueueUpdated;


            ArmyInfoButton.onClick.AddListener(() =>
            {
                ScreensManager.ShowScreen<ArmyInfoScreen.ArmyInfoScreen>();
            });


            OpenNewUnitBonus.unitOpened += OpenNewUnitBonus_unitOpened;
            _dayChangeSystem.NewDayCome += OnNewDayComes;

        }
        private void OnDestroy()
        {
            OpenNewUnitBonus.unitOpened -= OpenNewUnitBonus_unitOpened;
            _dayChangeSystem.NewDayCome -= OnNewDayComes;

            _armyRecruitSystem.StartTraining -= OnTrainingStarts;
            _armyRecruitSystem.TrainingProgress -= OnTrainingProgress;
            _armyRecruitSystem.UnitTrained -= OnTrained;

            _armyRecruitSystem.QueueUpdated -= OnQueueUpdated;
        }

        private void OnQueueUpdated()
        {
            productionQueue.SetItems<EntityButton>(_armyRecruitSystem.trainingQueue.QueueLenght, (item, param) =>
            {
                var queueItem = _armyRecruitSystem.trainingQueue.queue[param.index];
                item.SetInfo(queueItem.unit, (ent) => { }, queueItem.count);
            });
        }

        private void OnTrainingStarts(BattleUnitEntity unit)
        {
            recruitProgressView.Show();
            recruitProgressView.SetInfo(unit.Icon, $"{unit.ProduseTimeSec}s left", 0);
        }

        private void OnTrainingProgress(float timeLeft, float progress)
        {
            recruitProgressView.UpdateProgress($"{timeLeft}s left", progress);
        }

        private void OnTrained(BattleUnitEntity unit)
        {
            recruitProgressView.UpdateProgress("", 1);
            recruitProgressView.Hide();

            inArmyUnitsList.SetItems<EntityButton>(_armySystem.freeArmy.UnitTypesCount, (item, par) =>
            {
                var playerUnit = _armySystem.freeArmy.units[par.index].prototype;
                var count = _armySystem.freeArmy.units[par.index].count;

                item.SetInfo(playerUnit, OnArmyClick, count);
            });
        }

        private void OpenNewUnitBonus_unitOpened(BattleUnitEntity openUnit)
        {
            recruitUnitsList.AddItem<EntityButton>((item, param) =>
            {
                item.SetInfo(openUnit, OnArmyClick);
            });
        }

        public void OnArmyClick(GameEntity unit)
        {
            ScreensManager.ShowScreen<EntityInfoScreen>().ShowUnitUI((BattleUnitEntity)unit,
                _armyRecruitSystem.ReadyToRecruit.Contains((BattleUnitEntity)unit),
                _armySystem.freeArmy.GetUnitCount((BattleUnitEntity)unit));
        }

        private void OnNewDayComes(int obj)
        {
            freePeoplesText.text = _populationSystem.FreePeoples.ToString();
        }
    }
}