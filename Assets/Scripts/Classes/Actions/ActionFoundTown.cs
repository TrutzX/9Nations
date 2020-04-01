using Buildings;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Classes.Actions
{
    public class ActionFoundTown : BasePerformAction
    {
        public ActionFoundTown() : base("foundTown"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            //create found window
            WindowPanelBuilder win = WindowPanelBuilder.Create("Found a new town");
            /*GameObject name = win.panel.AddInput("town name",NGenTown.GetTownName(PlayerMgmt.ActPlayer().Nation().TownNameGenerator),s => {});
            win.panel.AddButton("Generate new name", () =>
            {
                name.GetComponent<InputField>().text = NGenTown.GetTownName(PlayerMgmt.ActPlayer().Nation().TownNameGenerator);
                //name.GetComponentsInChildren<Text>()[0].text = NGenTown.GetTownName(PlayerMgmt.Get().GetActPlayer().nation);
                //name.GetComponentsInChildren<Text>()[1].text = NGenTown.GetTownName(PlayerMgmt.Get().GetActPlayer().nation);
            }, "random", "random");*/

            InputField townName = win.panel.AddInputRandom("town name",
                LClass.s.nameGenerators[PlayerMgmt.ActPlayer().Nation().TownNameGenerator].Gen(), s => { },
                () => LClass.s.nameGenerators[PlayerMgmt.ActPlayer().Nation().TownNameGenerator].Gen());
            
            win.panel.AddButton("Found the town", () => { win.Close();FoundTown(info, pos, townName.text); }, DataAction().sound, DataAction().Icon);
            win.Finish(400);
        }

        protected void FoundTown(MapElementInfo info, NVector pos, string townName)
        {
            S.Towns().Create(townName, pos);
            OnMapUI.Get().UpdatePanel(pos);
            PlayerMgmt.ActPlayer().UpdateButtonMenu();
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);

            //add time?
            if (!string.IsNullOrEmpty(setting))
            {
                conf.data[setting] = "true";
            }

            return conf;
        }
    }
}