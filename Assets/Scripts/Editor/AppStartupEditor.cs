using KM.Startup;
using UnityEditor;
using UnityEngine;

namespace KM.Editors
{
    [CustomEditor(typeof(AppStartup))]
    public class AppStartupEditor : Editor
    {
        private AppStartup _target;

        private const int _pixOnLine = 15;
        private void OnEnable()
        {
            _target = target as AppStartup;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;

            EditorGUILayout.LabelField("Current State: " + _target.State.GetType().Name, EditorStyles.boldLabel);

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Systems:", EditorStyles.boldLabel);


            foreach (var system in _target.Systems.Values)
            {
                if (GUILayout.Button($"    - {system.GetType().Name}", GUILayout.MinHeight(_pixOnLine)))
                {
                    ShowSystemInfo(system);
                }
            }

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Update Systems:", EditorStyles.boldLabel);

            var systemsList = "\n";
            int height = 0;
            foreach (var system in _target.Systems.Values)
            {
                if (system is ISystemUpdate)
                {
                    height++;
                    systemsList += $"    - {system.GetType().Name} \n";
                }
            }

            EditorGUILayout.LabelField(systemsList, GUILayout.MinHeight(height * _pixOnLine));

        }

        private void ShowSystemInfo(ISystem system)
        {
            var go = new GameObject(system.GetType().Name);
            var systemView = go.AddComponent<SystemView>();

            systemView.system = system;

            Selection.activeGameObject = go;
        }
    }
}