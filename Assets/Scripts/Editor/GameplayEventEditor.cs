using KM.Features.GameEventsFeature.Events;
using KM.Features.GameEventsFeature.Events.Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(GameplayEvent), true)]
public class GameplayEventEditor : EditorWithSubEditors<ConditionEditor, Condition>
{
    public GameplayEvent editedEvent;



    string[] conditionsTypesNames;
    public int selectedIndex { get; private set; }

    private void OnEnable()
    {
        if (target == null) { 
            DestroyImmediate(this);
            return;
    }
        editedEvent = target as GameplayEvent;

        CheckAndCreateSubEditors(editedEvent.requiredConditions);

        EditorExtensions.SetTypeNamesArray(typeof(Condition), ref conditionsTypesNames);
    }

    protected override void SubEditorSetup(ConditionEditor editor)
    {
        editor.condition = editor.target as Condition;

        
    }

  

    public override void OnInspectorGUI()
    {     
        if (editedEvent == null)
            return;

        EditorGUILayout.BeginVertical(GUI.skin.box);        

        CheckAndCreateSubEditors(editedEvent.requiredConditions);

        EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

      
        selectedIndex = EditorGUILayout.Popup(selectedIndex, conditionsTypesNames, GUILayout.Width(200));
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            EditorExtensions.AddObjectOfTypeToArray(conditionsTypesNames[selectedIndex], ref editedEvent.requiredConditions, editedEvent);
            //AddEvent();
        }

        EditorGUILayout.EndHorizontal();

        if (subEditors != null)
        {
            for (int i = 0; i < subEditors.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(subEditors[i].target.name, EditorStyles.boldLabel);

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Remove"))
                {
                    EditorExtensions.RemoveObjectFromArray(subEditors[i].condition, ref editedEvent.requiredConditions, editedEvent);
                }
                EditorGUILayout.EndHorizontal();

                subEditors[i]?.OnInspectorGUI();
            }
        }

        base.OnInspectorGUI();

        EditorGUILayout.EndVertical();

      
    }
}
