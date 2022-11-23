using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KM.Features.Resources;

[CustomPropertyDrawer(typeof(ResourceStorage))]
public class ResourceStoragePropertyDrawer : PropertyDrawer
{
    public readonly int _resourcesCount = (typeof(ResourceType)).GetEnumNames().Length;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //  base.OnGUI(position, property, label);

        EditorGUI.indentLevel++;

        position.height = 15;

        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

        //EditorGUI.LabelField(position, label, EditorStyles.boldLabel);
        if (property.isExpanded)
        {
            SerializedProperty recources = property.FindPropertyRelative("resources");

            for (int i = 0; i < _resourcesCount; i++)
            {
                position.y += 18;

                string resourceType = ((ResourceType)i).ToString();
                recources.GetArrayElementAtIndex(i).intValue = EditorGUI.IntField(position, new GUIContent(resourceType), recources.GetArrayElementAtIndex(i).intValue);
            }
        }
        EditorGUI.indentLevel--;
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return property.isExpanded ? _resourcesCount * 22 : 22;
    }
}
