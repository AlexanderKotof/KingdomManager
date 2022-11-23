using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalableLayout : MonoBehaviour
{
    public RectTransform thisTransform;
    public RectTransform groupTransform;
    public LayoutGroup parentTransform;

    public GridLayoutGroup group1;

    int lastChildCount = 0;
    float baseGroupHeight = 0;
    float baseContainerHeight = 0;

    public bool ScaleHoryzontal = false;
    public bool scaleVertical = true;

    void Start()
    {
        thisTransform = (RectTransform)transform;

        parentTransform = thisTransform.parent.GetComponent<LayoutGroup>();

        group1 = GetComponentInChildren<GridLayoutGroup>();
        groupTransform = (RectTransform)group1.transform;


        baseGroupHeight = groupTransform.rect.height;
        baseContainerHeight = thisTransform.rect.height;

        //lastChildCount = groupTransform.childCount;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lastChildCount != groupTransform.childCount)
        {
            int modify = Mathf.CeilToInt((float)groupTransform.childCount / group1.constraintCount);

            //var rect = groupTransform.rect;
            //rect.height = baseGroupHeight + modify * group1.cellSize.y;

            // groupTransform.rect.height = rect;
            Debug.Log("Update Height " + baseContainerHeight + modify * (group1.cellSize.y + group1.spacing.y));

            groupTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, baseGroupHeight + modify * (group1.cellSize.y + group1.spacing.y / 2));
            thisTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, baseContainerHeight + modify * (group1.cellSize.y + group1.spacing.y / 2));

            parentTransform.padding = new RectOffset();

             lastChildCount = groupTransform.childCount;

        }
    }
}
