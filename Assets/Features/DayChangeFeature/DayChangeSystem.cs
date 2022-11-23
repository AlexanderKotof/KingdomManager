using KM.Startup;
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
            AppStartup.Instance.StartCoroutine(DaysCounter());
        }

        public void Destroy()
        {
            AppStartup.Instance.StopCoroutine(DaysCounter());
        }

        IEnumerator DaysCounter()
        {
            while (AppStartup.Instance.isActiveAndEnabled)
            {
                yield return _dayChangeTime;

                CurrentDay++;

                NewDayCome?.Invoke(CurrentDay);
            }
        }
    }
}