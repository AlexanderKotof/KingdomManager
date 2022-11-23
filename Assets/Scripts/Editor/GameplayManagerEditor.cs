using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameplayManagerEditor //: EditorWithSubEditors<GameplayEventEditor, GameplayEvent>
{
    /*GameplayEventsSystem manager;

    string[] eventTypesNames;
    private bool Expanded;

    public int selectedIndex { get; private set; }

    private void OnEnable()
    {
        //manager = target as GameplayEventsSystem;
        //CheckAndCreateSubEditors(manager.GameplayEvents);

        EditorExtensions.SetTypeNamesArray(typeof(GameplayEvent), ref eventTypesNames);
    }

    protected override void SubEditorSetup(GameplayEventEditor editor)
    {
        
    }

    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUI.indentLevel++;

        Expanded = EditorGUILayout.Foldout(Expanded, "Events", true, EditorStyles.foldoutHeader);

        //EditorGUILayout.LabelField("Bonuses", EditorStyles.boldLabel);
        if (Expanded)
        {
            EditorGUILayout.BeginHorizontal();

            selectedIndex = EditorGUILayout.Popup(selectedIndex, eventTypesNames, GUILayout.Width(200));
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("+", GUILayout.Width(30)))
            {

                AddEvent(eventTypesNames[selectedIndex]);


            }

            EditorGUILayout.EndHorizontal();

            CheckAndCreateSubEditors(manager.GameplayEvents);

            if (subEditors != null)
                for (int i = 0; i < subEditors.Length; i++)
                {
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField((i + 1) + " " + subEditors[i].target.name, EditorStyles.boldLabel);

                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Remove"))
                    {
                        EditorExtensions.RemoveObjectFromArray(subEditors[i].editedEvent, ref manager.GameplayEvents, manager);
                    }
                    EditorGUILayout.EndHorizontal();


                    subEditors[i]?.OnInspectorGUI();

                }
        }

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
    }


    public void AddEvent(string type) 
    {
        var _event = Create(type);

        Undo.RecordObject(manager, "Created new event");

        //AssetDatabase.AddObjectToAsset(_event, serializedObject.context);
        //AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(_event));

        ArrayUtility.Add(ref manager.GameplayEvents, _event); // nice 

        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(manager);
    }

    public GameplayEvent Create(string typeName)
    {
        GameplayEvent newEvent = CreateInstance(typeName) as GameplayEvent;
        newEvent.hideFlags = HideFlags.HideInHierarchy;
        newEvent.name = typeName.Normalize(System.Text.NormalizationForm.FormC);
        return newEvent;
    }

    public void Remove(GameplayEvent Event)
    {
        Undo.RecordObject(manager, "Removing hint");

        ArrayUtility.Remove(ref manager.GameplayEvents, Event);

        DestroyImmediate(Event, true);
        //AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(manager);

        //SetAllHintsDescriptions();

    }*/
}
