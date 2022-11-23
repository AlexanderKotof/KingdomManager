using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScallableUIContainer : MonoBehaviour
{
    RectTransform[] childsTransform;
    RectTransform thisTransfom;

    float currentHeight = 0;

    float minHeight;

    void Start()
    {
        childsTransform = new RectTransform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childsTransform[i] = (RectTransform)transform.GetChild(i);

            if(childsTransform[i].gameObject.activeSelf)
            currentHeight += childsTransform[i].rect.height;
        }

        minHeight = GetComponentInParent<RectTransform>().rect.height / 3;

        if (currentHeight < minHeight)
        {
            currentHeight = minHeight;
        }

        thisTransfom = (RectTransform)transform;

        thisTransfom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);

       // 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentHeight = 0;

        for (int i = 0; i < childsTransform.Length; i++)
        {
            if (childsTransform[i].gameObject.activeSelf)
                currentHeight += childsTransform[i].rect.height;
        }

        if (currentHeight < minHeight)
        {
            currentHeight = minHeight;
        }

        if (thisTransfom.rect.height != currentHeight) 
        thisTransfom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
    }
}
