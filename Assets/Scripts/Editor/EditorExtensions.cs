using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorExtensions
{
    public static void SetTypeNamesArray(System.Type baseType, ref string[] TypesNames)
    {
        System.Type[] allTypes = baseType.Assembly.GetTypes();

        List<System.Type> SubTypeList = new List<System.Type>();

        if (!baseType.IsAbstract && !baseType.IsInterface)
            SubTypeList.Add(baseType);

        for (int i = 0; i < allTypes.Length; i++)
        {
            if (allTypes[i].IsSubclassOf(baseType) && !allTypes[i].IsAbstract)
            {
                SubTypeList.Add(allTypes[i]);
            }
        }

        TypesNames = new string[SubTypeList.Count];

        for (int i = 0; i < SubTypeList.Count; i++)
        {
            TypesNames[i] = SubTypeList[i].Name;
        }
    }

    public static T AddObjectOfTypeToArray<T>(string type, ref T[] array, UnityEngine.Object objectTosave ) where T : ScriptableObject
    {
        T newElement = CreateObject(type) as T;

        Undo.RecordObject(objectTosave, "Created new object");

        ArrayUtility.Add(ref array, newElement); // nice 

        var _type = objectTosave.GetType();

        AssetDatabase.SaveAssets();

        if (_type.IsSubclassOf(typeof(ScriptableObject)) || _type == typeof(ScriptableObject))
        { 
            AssetDatabase.AddObjectToAsset(newElement, objectTosave);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newElement));

           
        }     

        EditorUtility.SetDirty(objectTosave);

        return newElement;
    }

    public static ScriptableObject CreateObject(string typeName)// where T : ScriptableObject
    {
        ScriptableObject newElement = ScriptableObject.CreateInstance(typeName);
        newElement.hideFlags = HideFlags.HideInHierarchy;
        newElement.name = typeName.Normalize(System.Text.NormalizationForm.FormC);

        return newElement;
    }

    public static void RemoveObjectFromArray<T>(T objectToRemove, ref T[] array, UnityEngine.Object objectTosave) where T : ScriptableObject
    {
        Undo.RecordObject(objectTosave, "Removing object");

        ArrayUtility.Remove(ref array, objectToRemove);

        UnityEngine.Object.DestroyImmediate(objectToRemove, true);

        var type = objectTosave.GetType();

        if (type.IsSubclassOf(typeof(ScriptableObject)) || type == typeof(ScriptableObject))
        {
            AssetDatabase.SaveAssets();
        }

        EditorUtility.SetDirty(objectTosave);



    }
    
}
