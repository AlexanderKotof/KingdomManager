using KM.UI.CarouselScreens;
using ScreenSystem;
using ScreenSystem.Screens;
using UnityEngine;

namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    [CreateAssetMenu(menuName = "Game Entities/Events/Bonuses/Show New Screen")]
    public class OpenNewScreenBonus : Bonus
    {
        public BaseScreen ScreenObjectPrefab;
        public int order;

        public bool carousel;

        public override string GetDescription()
        {
            return null;
        }

        public override void Activate()
        {
            Debug.Log("Activate " + name);
            //create menu in UI manager
            if (carousel)
                ScreensManager.GetScreen<CarouselScreen>().AddNewMenu(ScreenObjectPrefab, order);
            else
            {
                ScreensManager.ShowScreen(ScreenObjectPrefab);
            }
        }

        public override void Deactivate()
        {

        }
    }
}
