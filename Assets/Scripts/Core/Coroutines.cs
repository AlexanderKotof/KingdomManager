using System.Collections;
using UnityEngine;

namespace KM.Core
{
    public class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        public static Coroutine Run(IEnumerator coroutine)
        {
            return _instance?.StartCoroutine(coroutine);
        }

        public static void Stop(IEnumerator coroutine)
        {
            _instance?.StopCoroutine(coroutine);
        }

        public static void StopAll()
        {
            _instance?.StopAllCoroutines();
        }
    }
}