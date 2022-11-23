using KM.Features.GameEventsFeature.Events.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*//[CustomEditor(typeof(BuildingBase))]
public class BuildingEditor : Editor
{
    BuildingBase building;
    Bonus bonuses;
    Editor bonusEditor;

    private void OnEnable()
    {
        building = target as BuildingBase;

        if (building.ProvideBonuses == null || building.ProvideBonuses.bonuses == null)
        {
            Debug.Log("crreate bonuses");
            building.ProvideBonuses = CreateInstance<BonusesArray>();

            building.ProvideBonuses.hideFlags = HideFlags.HideInHierarchy;

            AssetDatabase.AddObjectToAsset(building.ProvideBonuses, building);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(building.ProvideBonuses));
            AssetDatabase.SaveAssets();
        }
           

        bonusEditor = CreateEditorWithContext(new Object[]{ building.ProvideBonuses }, building);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(bonusEditor != null)
        {
            bonusEditor.OnInspectorGUI();
        }
    }
}
*/