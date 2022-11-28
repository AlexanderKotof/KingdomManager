using KM.BattleSystem;
using KM.Core;
using KM.Features.Population;
using KM.Features.Resources;
using KM.Startup;
using KM.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.ArmyFeature
{
    public class ArmySystem : ISystem
    {
        public Army freeArmy;

        public Army guardArmy;

        public List<General> availableGenerals;

        public List<BattleUnitEntity> ReadyToRecruit = new List<BattleUnitEntity>();

        public ProductionQueue trainingQueue;

        float recruitTimeLeft = 0;
        bool isRecruiting = false;

        public event Action<BattleUnitEntity> StartTraining;
        public event Action<float, float> TrainingProgress;
        public event Action<BattleUnitEntity> UnitTrained;
        public event Action QueueUpdated;

        public event Action ArmyUpdated;


        private PopulationSystem _populationSystem;
        private ResourcesSystem _resourcesSystem;

        public ArmySystem()
        {
            trainingQueue = new ProductionQueue();

            freeArmy = new Army();
            guardArmy = new Army();
        }

        public int GetAllUnitsCount()
        {
            return freeArmy.GetAllUnitsCount() + guardArmy.GetAllUnitsCount();
        }

        public void Initialize()
        {
            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
            _resourcesSystem = GameSystems.GetSystem<ResourcesSystem>();
        }

        public void Destroy()
        {

        }

        public void RecruitUnit(BattleUnitEntity unit)
        {
            RecruitUnit(ReadyToRecruit.IndexOf(unit));
        }
        public void RecruitUnit(int index)
        {
            if (ReadyToRecruit.Count <= index || index < 0)
            {
                Debug.Log("Error " + ReadyToRecruit.Count + "<=" + index);
                return;
            }

            var unit = ReadyToRecruit[index];

            if (_resourcesSystem.Resources.HasResources(unit.ProduseCost))
            {
                _resourcesSystem.ChangeResources(unit.ProduseCost.Invert());
            }
            else
            {
                Debug.Log("Dont have resources!");
                return;
            }

            trainingQueue.AddUnit(unit);

            QueueUpdated?.Invoke();

            if (!isRecruiting)
                Coroutines.Run(Recruit());
        }

        public void MoveToDefence(BattleUnitEntity selectedUnit, Vector3 position)
        {
            freeArmy.RemoveUnits(selectedUnit, 1);
            guardArmy.AddUnit(selectedUnit, position);

            ArmyUpdated?.Invoke();
        }

        public void MoveToArmy(BattleUnitEntity selectedUnit, int count)
        {
            freeArmy.AddUnits(selectedUnit, count);
            guardArmy.RemoveUnits(selectedUnit, count);

            ArmyUpdated?.Invoke();
        }

        public int GetGoldConsuming()
        {
            int price = 0;
            foreach (var unit in freeArmy.units)
                price += unit.count;

            return price;
        }

        /*
        public void CancelTraining()
        {
            MainGameManager.Instance.Resources.AddResources(RecruitQuerry[0].ProduseCost);
            MainGameManager.Instance.Peoples++;

            StopCoroutine(Recruit());

            onUnitTrained?.Invoke(null);

            RecruitQuerry.RemoveAt(0);

            StartCoroutine(Recruit());
        }*/

        public void SpeedUp()
        {
            recruitTimeLeft = 0;
        }

        IEnumerator Recruit()
        {
            while (trainingQueue.QueueLenght > 0)
            {
                while (_populationSystem.FreePeoples <= 0)
                {
                    yield return null;
                }

                var queueItem = trainingQueue.GetFirst();

                if (queueItem == null)
                    break;

                Debug.Log("Recruiting " + queueItem.unit.name + "...");

                _populationSystem.ChangePeopleCount(-1);

                StartTraining?.Invoke(queueItem.unit);

                isRecruiting = true;

                recruitTimeLeft = queueItem.unit.ProduseTimeSec;

                while (recruitTimeLeft > 0)
                {
                    yield return new WaitForSeconds(1);

                    recruitTimeLeft--;

                    TrainingProgress?.Invoke(recruitTimeLeft, (queueItem.unit.ProduseTimeSec - recruitTimeLeft) / queueItem.unit.ProduseTimeSec);
                }

                freeArmy.AddUnits(queueItem.unit, 1);

                trainingQueue.RemoveOneFromFirst();

                QueueUpdated?.Invoke();

                UnitTrained?.Invoke(queueItem.unit);

                ArmyUpdated?.Invoke();
            }

            isRecruiting = false;
        }

    }
}