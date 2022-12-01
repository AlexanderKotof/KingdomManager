using KM.Startup.StateMachine;
using KM.Systems;
using KM.UI.CarouselScreens;
using KM.UI.EndGameScreen;
using ScreenSystem;
using UnityEngine;

namespace KM.Startup
{
    public static class AppStartupUtils
    {
        public static void RestartGame()
        {
            ScreensManager.HideAll();
            ScreensManager.DestroyScreen<CarouselScreen>();
            AppStateMachine.RestartGame();
        }

        public static void EndGame(string message)
        {
            ScreensManager.ShowScreen<EndGameScreen>().SetInfo(message);
            GameSystems.DestroySystems();
        }

        public static async void QuitGame()
        {
            //save game state
            await SaveGameDataUtils.SaveData();

            Application.Quit();
        }
    }
}