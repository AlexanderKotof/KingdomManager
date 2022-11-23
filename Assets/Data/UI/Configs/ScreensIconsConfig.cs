using KM.UI.CarouselScreens;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KM.UI.Configs
{
    public class ScreensIconsConfig : ScriptableObject
    {
        [Serializable]
        public struct ScreenIcon
        {
            public GameScreen screen;
            public Sprite icon;
        }

        public List<ScreenIcon> screenIcons;
    }
}