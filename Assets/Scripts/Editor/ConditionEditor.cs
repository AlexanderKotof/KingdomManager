using KM.Features.GameEventsFeature.Events.Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(Condition), true)]
public class ConditionEditor : Editor
{
    public Condition condition;

    public override void OnInspectorGUI()
    {
        if (target == null)
            DestroyImmediate(this);

        EditorGUILayout.BeginVertical(GUI.skin.box);

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();
    }
}
