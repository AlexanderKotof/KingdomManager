using KM.Features.ArmyFeature;
using KM.Systems;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{

    public Image icon;

    public Text nameText;

    public Text descriptionText;

    public Button AddAllToArmyButton;
    public Button CloseButton;

    public Transform recruitedUnitsParent;

    General general;



    /*
    public static void ShowUI(General general)
    {
        var instance = Instantiate(Prefab, CarouselScreen.canvasTransform).GetComponent<GeneralUI>();

        instance.transform.SetSiblingIndex(2);

        instance.Initialize(general);
        //instance.AddAllToArmyButton.gameObject.SetActive(false);

    }*/

    private void Initialize(General general)
    {
        icon.sprite = general.Icon;

        this.general = general;
        //instance.countText.text = count > 0 ? count.ToString() : "";

        nameText.text = general.name;

        descriptionText.text = general.GetDescription();

       

        AddAllToArmyButton.onClick.AddListener(() =>
        {
            //general.Army.AddUnits(MainGameManager.ArmyManager.army);
            GameSystems.GetSystem<ArmySystem>().freeArmy = new Army();

            for (var i = 0; i < recruitedUnitsParent.childCount; i++)
            {
                Destroy(recruitedUnitsParent.GetChild(i).gameObject);
            }

            Init();

        });

        CloseButton.onClick.AddListener(Close);

        Init();
    }

    private void Init()
    {

        var manager = GameSystems.GetSystem<ArmySystem>();

        for (int i = 0; i < manager.freeArmy.UnitTypesCount; i++)
        {
            var unit = manager.freeArmy.GetUnitByIndex(i, out int count);
           // var button = EntityButton.CreateEntityButton(unit, recruitedUnitsParent, null, count);
        }
    }


    public void Close()
    {
        Destroy(gameObject);
    }
}
