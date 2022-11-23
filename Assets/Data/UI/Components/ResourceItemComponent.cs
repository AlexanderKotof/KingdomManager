using ScreenSystem.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KM.UI.Components
{
    public class ResourceItemComponent : WindowComponent
    {
        public TMP_Text resourceName;
        public TMP_Text count;
        public TMP_Text change;

        public void SetInfo(string name, int count)
        {
            this.resourceName.text = name;
            this.count.text = count.ToString();

            var color = change.color;
            color.a = 0;
            change.color = color;
        }

        public IEnumerator FadeOut(int changeValue)
        {
            var color = change.color;
            color.a = 1;
            change.color = color;
            change.text = changeValue.ToString();
            while (color.a > 0)
            {
                color.a -= Time.deltaTime;
                change.color = color;
                yield return null;
            }
        }
    }
}