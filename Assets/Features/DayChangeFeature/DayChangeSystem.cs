using KM.Core;
using KM.Startup;
using KM.Systems;
using System;
using System.Collections;
using UnityEngine;

namespace KM.Features.DayChange
{
    public class DayChangeSystem : ISystem
    {
        public int CurrentDay { get; private set; }

        readonly WaitForSeconds _dayChangeTime;

        public event Action<int> NewDayCome;

        public DayChangeSystem(float secondsInDay, int day = 0)
        {
            _dayChangeTime = new WaitForSeconds(secondsInDay);
            CurrentDay = day;
        }

        public void Initialize()
        {
            Coroutines.Run(DaysCounter());
        }

        public void Destroy()
        {
            Coroutines.Stop(DaysCounter());
        }

        IEnumerator DaysCounter()
        {
            while (true)
            {
                yield return _dayChangeTime;

                CurrentDay++;

                NewDayCome?.Invoke(CurrentDay);
            }
        }
    }
}