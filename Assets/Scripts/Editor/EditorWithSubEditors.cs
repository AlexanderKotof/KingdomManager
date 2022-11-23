using UnityEngine;
using UnityEditor;

public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor
    where TEditor : Editor
    where TTarget : Object
{
    protected TEditor[] subEditors;


    protected void CheckAndCreateSubEditors(TTarget[] subEditorTargets)
    {
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;

        CleanupEditors();

        subEditors = new TEditor[subEditorTargets.Length];

       // Debug.Log(subEditors.Length);

        if (subEditors.Length > 0)
            for (int n = 0; n < subEditors.Length; n++)
            {
                subEditors[n] = CreateEditor(subEditorTargets[n]) as TEditor;
                SubEditorSetup(subEditors[n]);
            }
    }


    protected void CleanupEditors ()
    {
        if (subEditors == null)
            return;

        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate (subEditors[i]);
        }

        subEditors = null;
    }


    protected abstract void SubEditorSetup (TEditor editor);
}
/*
public abstract class EditorWindowWithSubEditors<TEditor1, TEditor2, TTarget1, TTarget2> : Editor
    where TEditor1 : Editor
    where TEditor2 : Editor

    where TTarget1 : Object
    where TTarget2 : Object
{
    protected Editor[] subEditors;
    

    protected void CheckAndCreateSubEditors(TTarget1[] subEditorTargets)
    {
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;

        CleanupEditors();

        subEditors = new TEditor[subEditorTargets.Length];

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor(subEditorTargets[i]) as TEditor;
            SubEditorSetup(subEditors[i]);
        }
    }
    protected void CheckAndCreateSubEditors(TTarget1[] subEditorTargets1, TTarget2[] subEditorTargets2)
    {
        if (subEditors != null && subEditors.Length == subEditorTargets1.Length)
            return;

        CleanupEditors();

        subEditors = new TEditor[subEditorTargets.Length];

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor(subEditorTargets[i]) as TEditor;
            SubEditorSetup(subEditors[i]);
        }


        protected void CleanupEditors()
    {
        if (subEditors == null)
            return;

        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate(subEditors[i]);
        }

        subEditors = null;
    }


    protected abstract void SubEditorSetup(TEditor editor);
}
*/