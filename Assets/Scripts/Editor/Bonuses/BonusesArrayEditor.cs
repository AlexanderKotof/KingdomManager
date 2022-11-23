using KM.Features.GameEventsFeature.Events.Bonuses;
using System;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(BonusesArray))]
public class BonusesArrayEditor : EditorWithSubEditors<BonusEditor, Bonus>
{
    BonusesArray bonusesArray;
    SerializedProperty bonusesProperty;

    string[] bonusesTypesNames;
    int selectedIndex = 0;

    
    private bool Expanded;

    private void OnEnable()
    {
        bonusesArray = target as BonusesArray;

        EditorExtensions.SetTypeNamesArray(typeof(Bonus) as Type, ref bonusesTypesNames);

        CheckArray();
    }

    void CheckArray()
    {
        if (!bonusesArray || bonusesArray.bonuses == null)
            return;

        for (int i = 0; i < bonusesArray.bonuses.Length; i++)
        {
            if(bonusesArray.bonuses[i] == null)
            {
                EditorExtensions.RemoveObjectFromArray(bonusesArray.bonuses[i], ref bonusesArray.bonuses, serializedObject.context);
            }
        }
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        if (bonusesArray == null)
            return;

        

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUI.indentLevel++;

        Expanded = EditorGUILayout.Foldout(Expanded, "Bonuses", true, EditorStyles.foldoutHeader);

        //EditorGUILayout.LabelField("Bonuses", EditorStyles.boldLabel);
        if (Expanded)
        {
            EditorGUILayout.BeginHorizontal();

            selectedIndex = EditorGUILayout.Popup(selectedIndex, bonusesTypesNames, GUILayout.Width(200));
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("+", GUILayout.Width(30)))
            {
                EditorExtensions.AddObjectOfTypeToArray(bonusesTypesNames[selectedIndex], ref bonusesArray.bonuses, serializedObject.context);
            }

            EditorGUILayout.EndHorizontal();

            CheckAndCreateSubEditors(bonusesArray.bonuses);

            if(subEditors!=null)
            for (int i = 0; i < subEditors.Length; i++)
            {
                EditorGUILayout.Space();

                subEditors[i]?.OnInspectorGUI();

            }
        }

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
    }
    
    protected override void SubEditorSetup(BonusEditor editor)
    {
        if (editor == null || editor.target == null)
        {
            DestroyImmediate(editor);
            return;
        }
        editor.bonus = editor.target as Bonus;

        editor.onRemove += () => {
            EditorExtensions.RemoveObjectFromArray(editor.bonus, ref bonusesArray.bonuses, serializedObject.context);
        };
    }
}
