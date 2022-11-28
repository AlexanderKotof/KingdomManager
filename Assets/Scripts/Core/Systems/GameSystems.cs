using System;
using System.Collections.Generic;

namespace KM.Systems
{
    public class GameSystems
    {
        private static GameSystems _instance;

        public static Dictionary<Type, ISystem> Systems => _instance._systems;

        private Dictionary<Type, ISystem> _systems;

        private readonly List<ISystemUpdate> _updateSystems;

        public GameSystems()
        {
            _systems = new Dictionary<Type, ISystem>();
            _updateSystems = new List<ISystemUpdate>();

            _instance = this;
        }

        public static void RegisterSystem(ISystem system)
        {
            _instance._systems.Add(system.GetType(), system);

            if (system is ISystemUpdate updateSystem)
            {
                _instance._updateSystems.Add(updateSystem);
            }
        }

        public static T GetSystem<T>() where T : ISystem
        {
            if (_instance._systems.TryGetValue(typeof(T), out var system))
                return (T)system;

            return default;
        }

        public void UpdateSystems()
        {
            foreach (var system in _updateSystems)
            {
                system.Update();
            }
        }

        public static void DestroySystems()
        {
            foreach (var system in _instance._systems.Values)
            {
                system.Destroy();
            }

            _instance._systems.Clear();
            _instance._updateSystems.Clear();
        }
    }
}