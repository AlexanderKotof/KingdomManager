using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Team))]
public class BattleUnitsPropertyDrawer : PropertyDrawer
{
    int arraySize = 1;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        var unitsProp = property.FindPropertyRelative("units");
        var unitsCount = property.FindPropertyRelative("unitCount");

        position.height = 15;

        EditorGUI.LabelField(position, label, EditorStyles.boldLabel);

        position.y += 20;

        unitsProp.arraySize = unitsCount.arraySize = arraySize  = EditorGUI.IntField(position, new GUIContent("Size"), unitsProp.arraySize);

        position.y += 20;


        var countPos = position;

        countPos.width /= 4;

        position.width = position.width / 2f;

        position.x += countPos.width + 30;


        for (int i = 0; i < unitsProp.arraySize; i++)
        {
            EditorGUI.PropertyField(position, unitsProp.GetArrayElementAtIndex(i), new GUIContent());

            var labelPos = position;
            labelPos.x -= 20;

            EditorGUI.LabelField(labelPos, "X", EditorStyles.boldLabel);

            EditorGUI.PropertyField(countPos, unitsCount.GetArrayElementAtIndex(i), new GUIContent());

            position.y += 22;
            countPos.y += 22;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 40 + arraySize * 22;
    }

}
