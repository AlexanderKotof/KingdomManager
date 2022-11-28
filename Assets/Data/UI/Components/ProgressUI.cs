using KM.Features.Population;
using KM.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    RectTransform thisTransform;

    public Image unitPic;
    public Slider progressSlider;
    public Text timeLeft;
    public Text unitNameText;
    public Text countText;

    public Button speedUpButton;

    public Text freePeoplesCount;

    public float openedHeight;
    public float closedHeight;

    private PopulationSystem _populationSystem;

    void Awake()
    {
        thisTransform = (RectTransform)transform;

        openedHeight = thisTransform.rect.height;
        closedHeight = openedHeight * 0.6f;

        _populationSystem = GameSystems.GetSystem<PopulationSystem>();
    }

    

    public void Show(bool value)
    {
        //gameObject.SetActive(value);

        unitPic.gameObject.SetActive(value);
        progressSlider.gameObject.SetActive(value);

        timeLeft.enabled = value;
        speedUpButton.gameObject.SetActive(value);

        if (!value)
        {
            countText.text = "";

            thisTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, closedHeight);
        }
        else
            thisTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, openedHeight);

    }

    public void onTrainingProgress(float arg1, float arg2)
    {
        progressSlider.value = (arg2 - arg1) / arg2;
        timeLeft.text = arg1 + " sec.";

    }

    public void onStartTraining(BattleUnitEntity obj)
    {       
        Show(true);

        countText.text = "x1";


        unitNameText.text = "Training " + obj.name + "...";
        unitPic.sprite = obj.Icon;

        progressSlider.value = 0;

        timeLeft.text = obj.ProduseTimeSec + " sec.";

        freePeoplesCount.text = _populationSystem.FreePeoples.ToString();
    }

    public void onStartProgress(string name, Sprite icon, int time)
    {
        Show(true);

        //countText.text = "x1";


        unitNameText.text = "Building " + name + "...";
        unitPic.sprite = icon;

        progressSlider.value = 0;

        timeLeft.text = time + " sec.";
    }


}
