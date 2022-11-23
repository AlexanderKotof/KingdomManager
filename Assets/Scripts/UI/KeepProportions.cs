using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepProportions : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();

        var rectTransform = (RectTransform)transform;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.y);
    }

   
}
