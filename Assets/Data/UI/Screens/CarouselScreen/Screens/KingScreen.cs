using KM.Features.GameEventsFeature.Events;
using ScreenSystem.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KM.UI.CarouselScreens
{
    public class KingScreen : GameScreen
    {
        public TMP_Text KingText;

        Dialog currentDialog;

        public ListComponent answersList;


        private void Awake()
        {
            KingDialogEvent.OnDialogActivated += ShowDialog;
        }

        private void OnDestroy()
        {
            KingDialogEvent.OnDialogActivated -= ShowDialog;
        }

        public void ShowDialog(Dialog dialog)
        {
            currentDialog = dialog;

            KingText.text = dialog.NPCText;

            answersList.SetItems<ButtonComponent>(currentDialog.answers.Length, (item, param) =>
            {
                item.SetCallback(() => currentDialog?.Answer(param.index));
            });
        }
    }
}

[System.Serializable]
public class Dialog
{
    public string NPCText;
    public string[] answers;

    public event System.Action<int> onAnswered;

    public void Answer(int no)
    {
        onAnswered?.Invoke(no);
    }
}