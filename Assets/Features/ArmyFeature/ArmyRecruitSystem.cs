using KM.Core;
using KM.Features.Population;
using KM.Features.Resources;
using KM.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KM.Features.ArmyFeature
{
    public class ArmyRecruitSystem : ISystem
    {
        public List<BattleUnitEntity> ReadyToRecruit = new List<BattleUnitEntity>();

        public ProductionQueue trainingQueue;

        float recruitTimeLeft = 0;
        bool isRecruiting = false;

        public event Action<BattleUnitEntity> StartTraining;
        public event Action<float, float> TrainingProgress;
        public event Action<BattleUnitEntity> UnitTrained;
        public event Action QueueUpdated;


        private PopulationSystem _populationSystem;
        private ResourcesSystem _resourcesSystem;
        private ArmySystem _armySystem;

        public ArmyRecruitSystem()
        {
            trainingQueue = new ProductionQueue();
        }

        public void Destroy()
        {
            
        }

        public void Initialize()
        {
            _populationSystem = GameSystems.GetSystem<PopulationSystem>();
            _resourcesSystem = GameSystems.GetSystem<ResourcesSystem>();
            _armySystem = GameSystems.GetSystem<ArmySystem>();
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

                UnitTrained?.Invoke(queueItem.unit);
                trainingQueue.RemoveOneFromFirst();
                QueueUpdated?.Invoke();

                _armySystem.AddFreeUnits(queueItem.unit, 1);
            }

            isRecruiting = false;
        }

    }
}