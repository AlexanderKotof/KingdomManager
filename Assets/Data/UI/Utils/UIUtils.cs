namespace KM.UI.Utils
{
    public static class UIUtils
    {
        public static void HideScreensOnBattleStarts()
        {
            ScreenSystem.ScreensManager.HideScreen<CarouselScreens.CarouselScreen>();
            ScreenSystem.ScreensManager.HideScreen<ArmySetupScreen.ArmySetupScreen>();
        }

        public static void ShowScreensOnBattleEnds()
        {
            ScreenSystem.ScreensManager.ShowScreen<CarouselScreens.CarouselScreen>();
            ScreenSystem.ScreensManager.ShowScreen<ArmySetupScreen.ArmySetupScreen>();
        }
    }
}