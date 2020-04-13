using System;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Players.Infos;
using Tools;
using UI;
using Units;
using Random = UnityEngine.Random;

namespace Classes.Actions
{
    public class ActionEvolve : BasePlayerPerformAction
    {
        public ActionEvolve() : base("evolve"){}

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            if (Random.Range(0, 100) >= (100/player.elements.elements.Count))
            {
                player.info.Add(new Info("The gods have not been smiling on you, you've not evolved.","religion"));
                return;
            }

            player.elements.points += 1;
        }

        public override void BuildPanel(ActionDisplaySettings sett)
        {
            base.BuildPanel(sett);
            sett.panel.AddSubLabel("Chance to evolve",(100/sett.player.elements.elements.Count)+"%");
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}