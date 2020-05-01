using Game;
using GameMapLevels;
using Players;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Classes.GameButtons
{
    public class MoveLevelGameButtonRun : BaseGameButtonRun
    {
        public MoveLevelGameButtonRun() : base ("moveLevel") { }

        protected override void Run(Player player)
        {
            WindowPanelBuilder wpb = WindowPanelBuilder.Create("Move level");
            foreach (var gml in S.Map().levels)
            {
                wpb.panel.AddButton(gml.name, () =>
                {
                    S.Map().view.View(gml.level);
                    wpb.Close();
                }).interactable = gml.level != S.Map().view.ActiveLevel;
            }
            
            wpb.AddClose();
            wpb.Finish();
            
        }
    }
}