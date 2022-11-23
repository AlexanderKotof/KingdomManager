using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace KM.Editors
{
    [CustomEditor(typeof(SystemView))]
    public class SystemViewEditor : Editor
    {
        private SystemView _target;

        private FieldInfo[] _properties;

        private void OnEnable()
        {
            _target = target as SystemView;
            _properties = _target.system.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            Debug.Log("Properties count " + _properties.Length);
        }

        public override void OnInspectorGUI()
        {
            foreach (var property in _properties)
            {
                EditorGUILayout.LabelField(property.Name, property.GetValue(_target.system).ToString());
            }
        }
    }
}