using KM.Features;
using KM.UI.CarouselScreens;
using KM.UI.EndGameScreen;
using ScreenSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace KM.Startup
{
    public class AppStartup : MonoBehaviour
    {
        public static AppStartup Instance { get; private set; }

        public Feature[] features;

        public IGameState State => _currentState;

        private IGameState _currentState;

        public PlayerDataInfo playerData;

        public Dictionary<Type, ISystem> Systems { get; private set; } = new Dictionary<Type, ISystem>();

        private readonly List<ISystemUpdate> _updateSystems = new List<ISystemUpdate>();

        private readonly GameState _homeState = new GameState("Home");

        private void Start()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;

            Application.quitting += OnApplicationQuitting;

            StartGame();
        }

        private void OnDestroy()
        {
            Application.quitting -= OnApplicationQuitting;
            foreach (var system in Systems.Values)
            {
                system.Destroy();
            }
        }

        private void Update()
        {
            foreach (var system in _updateSystems)
            {
                system.Update();
            }
        }

        private async void StartGame()
        {
            await SetState(new InitializationState());
            await SetState(new LoadHomeLocationState());
            await SetState(new CreateGameSystemsState(features));
            await SetState(new ShowBaseScreensState());
            await SetState(_homeState);
        }

        private void DestroySystems()
        {
            foreach (var system in Systems.Values)
            {
                system.Destroy();
            }

            Systems.Clear();
            _updateSystems.Clear();
        }

        private async Task SetState(IGameState state)
        {
            _currentState = state;
            await _currentState.Execute();
        }

        private async void OnApplicationQuitting()
        {
            await SaveGameDataUtils.SaveData();
        }

        public void RegisterSystem(ISystem system)
        {
            Systems.Add(system.GetType(), system);

            if (system is ISystemUpdate updateSystem)
            {
                _updateSystems.Add(updateSystem);
            }
        }

        public T GetSystem<T>() where T : ISystem
        {
            if (Systems.TryGetValue(typeof(T), out var system))
                return (T)system;

            return default;
        }

        public static class App
        {
            public static void RestartGame()
            {
                ScreensManager.HideAll();
                ScreensManager.DestroyScreen<CarouselScreen>();
                Instance.StartGame();
            }

            public static void EndGame(string message)
            {
                ScreensManager.ShowScreen<EndGameScreen>().SetInfo(message);
                Instance.DestroySystems();
            }

            public static async void QuitGame()
            {
                //save game state
                await SaveGameDataUtils.SaveData();

                Application.Quit();
            }
        }
    }
}