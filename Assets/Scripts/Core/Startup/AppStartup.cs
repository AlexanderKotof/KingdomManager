using KM.Features;
using KM.Startup.StateMachine;
using KM.Systems;
using UnityEngine;

namespace KM.Startup
{
    public class AppStartup : MonoBehaviour
    {
        public IGameState State => _appStateMachine.State;

        public PlayerDataInfo playerData;

        private GameSystems _gameSystems;
        private AppStateMachine _appStateMachine;


        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            _gameSystems = new GameSystems();
            _appStateMachine = new AppStateMachine();

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