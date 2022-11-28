using KM.Startup;
using ScreenSystem.Components;
using ScreenSystem.Screens;

namespace KM.UI.EndGameScreen
{
    public class EndGameScreen : BaseScreen
    {
        public TMPro.TMP_Text messageText;
        public TMPro.TMP_Text achievementsText;
        public ButtonComponent restartButton;

        protected override void OnInit()
        {
            restartButton.SetCallback(RestartGame);
        }

        private void RestartGame()
        {
            AppStartupUtils.RestartGame();
            Hide();
        }

        public void SetInfo(string message)
        {
            messageText.SetText(message);

            achievementsText.SetText("Days: XXX Population: XXX Resources, etc...");
        }

        protected override void OnShow()
        {

        }

        protected override void OnHide()
        {
            
        }
    }
}
