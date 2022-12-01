using KM.UI.BattleBeginsScreen;

namespace KM.UI.Utils
{
    public static class UIUtils
    {
        public static void HideScreensOnBattleStarts()
        {
            ScreenSystem.ScreensManager.HideScreen<CarouselScreens.CarouselScreen>();
            ScreenSystem.ScreensManager.HideScreen<ArmySetupScreen.ArmySetupScreen>();
            ScreenSystem.ScreensManager.ShowScreen<InBattleScreen>();
        }

        public static void ShowScreensOnBattleEnds()
        {
            ScreenSystem.ScreensManager.ShowScreen<CarouselScreens.CarouselScreen>();
            ScreenSystem.ScreensManager.ShowScreen<ArmySetupScreen.ArmySetupScreen>();
            ScreenSystem.ScreensManager.HideScreen<InBattleScreen>();

            ScreenSystem.ScreensManager.ShowScreen<BattleOverScreen>();
        }
    }
}