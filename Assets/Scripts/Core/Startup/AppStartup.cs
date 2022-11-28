using KM.Features;
using KM.Startup.StateMachine;
using KM.Systems;
using UnityEngine;

namespace KM.Startup
{
    public class AppStartup : MonoBehaviour
    {
        public Feature[] features;

        public IGameState State => AppStateMachine.State;

        public PlayerDataInfo playerData;

        private GameSystems _gameSystems;
        public static AppStateMachine AppStateMachine { get; private set; }


        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            _gameSystems = new GameSystems();
            AppStateMachine = new AppStateMachine(features);

            Application.quitting += OnApplicationQuitting;

            AppStateMachine.StartGame();
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            Application.quitting -= OnApplicationQuitting;
            GameSystems.DestroySystems();
        }

        private void Update()
        {
            _gameSystems.UpdateSystems();
        }

        private async void OnApplicationQuitting()
        {
            Debug.Log("OnApplicationQuiting");
            await SaveGameDataUtils.SaveData();
        }
    }
}