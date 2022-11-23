using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using KM.Features.GameEventsFeature.Events.Bonuses;

//[CustomEditor(typeof(Bonus), true)]
public class BonusEditor : Editor
{
    public Action onRemove;
    public Bonus bonus;

    private void OnEnable()
    {
        bonus = target as Bonus;
    }

    public override void OnInspectorGUI()
    {
        if (target == null)
            DestroyImmediate(this);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(target.name, EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("-", GUILayout.Width(30)))
        {
            if (onRemove != null)
                onRemove?.Invoke();
            else
            {
                AssetDatabase.RemoveObjectFromAsset(bonus);
              
                AssetDatabase.Refresh();
            }
               
        }
        EditorGUILayout.EndHorizontal();
        
        DrawDefaultInspector();


    }
}
