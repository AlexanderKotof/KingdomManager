using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(Bonuses), true)]
public class BonusPropertyDrawer : PropertyDrawer
{
    //override 

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var bonus = property.serializedObject.targetObject;

        
        
        base.OnGUI(position, property, label);

        // if (Event.current.type == EventType.Repaint)
        //   return;
        //property.

        if (property == null)
            return;

        var bonusProperty = property.FindPropertyRelative("bonuses");

        // Debug.Log(bonusProperty.isArray);

        position.y += position.height;


        if (bonusProperty.isArray && GUI.Button(position, "+"))
        {
            bonusProperty.arraySize += 1;
        }
        
       // if(bonusProperty.arraySize > 0)
        //GUILayout.Label(bonusProperty.GetArrayElementAtIndex(0).name);

    

    }
}
