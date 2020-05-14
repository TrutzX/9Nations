using System;
using Audio;
using Buildings;
using Classes;
using GameButtons;
using Players;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.GameButtons
{
    [Serializable]
    public class GameButton : BaseData
    {

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            req.BuildPanel(panel, "Requirements");
        }

        public void Call(Player player)
        {
            LClass.s.Button(id).Call(player);
        }


        public Button CreateImageButton(Transform transform, Player player, IMapUI text)
        {
            Button button = UIElements.CreateImageButton(LClass.s.Button(id).Sprite(player), transform, () => { Call(player); }, Sound);

            UIHelper.HoverEnter(button, () => text.ShowPanelMessage(LSys.tem.inputs.GameButtonName(this)),
                () => text.ShowPanelMessage(""));

            return button;
        }

        public Button CreateImageTextButton(Transform transform, Player player)
        {
            Button button = UIHelper.CreateImageTextButton(LSys.tem.inputs.GameButtonName(this), LClass.s.Button(id).Sprite(player), transform, () =>
            {
                NAudio.Play(Sound);
                Call(player);
            });
            return button;
        }
    }
}