using KM.UI.CarouselScreens;
using ScreenSystem;
using UnityEngine;
using UnityEngine.UI;

public class AttentionUIButton : MonoBehaviour
{
    static GameObject Prefab => Resources.Load<GameObject>("UI/AttentionButton");
    static CarouselScreen manager => ScreensManager.GetScreen<CarouselScreen>();

    public Button button;
    int targetScreen = 0;

    public static void Attention(int screenID)
    {
        var instance = Instantiate(Prefab, CarouselScreen.canvasTransform).GetComponent<AttentionUIButton>();

        instance.button.onClick.AddListener(() => {

            manager.GoToScreen(screenID);

            Destroy(instance.gameObject);
        });

        instance.targetScreen = screenID;

        manager.onScreenChanged += instance.Manager_onScreenChanged;
    }

    private void Manager_onScreenChanged(int obj)
    {
        if (targetScreen != obj)
            return;

        manager.onScreenChanged -= Manager_onScreenChanged;

        Destroy(gameObject);
    }
}
