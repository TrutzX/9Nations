using System;
using Buildings;
using Classes;
using GameButtons;
using InputAction;
using Players;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.GameButtons
{
    [Serializable]
    public class GameButton : BaseData
    {
        public string location;

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            req.BuildPanel(panel, "Requirements");
        }

        public void Call(Player player)
        {
            LClass.s.gameButtonRuns[id].Call(player);
        }


        public Button CreateImageButton(Transform transform, Player player, IMapUI text)
        {
            GameObject button = UIElements.CreateImageButton(Icon, transform, () => { Call(player); }, Sound);

            UIHelper.HoverEnter(button, () => text.ShowPanelMessage(InputKeyHelper.GameButtonName(this)),
                () => text.ShowPanelMessage(""));

            return button.GetComponent<Button>();
        }

        public Button CreateImageTextButton(Transform transform, Player player)
        {
            Button button = UIHelper.CreateImageTextButton(name, Sprite(), transform, () =>
            {
                NAudio.Play(Sound);
                Call(player);
            });
            return button;
        }
    }
}