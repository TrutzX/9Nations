using System.Linq;
using Buildings;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Towns;
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

            Button button = null;
            
            InputField townName = win.panel.AddInputRandom("town name",
                LClass.s.NameGenerator(S.ActPlayer().Nation().TownNameGenerator), s => { },
                () => LClass.s.NameGenerator(S.ActPlayer().Nation().TownNameGenerator));

            var coats = L.b.coats.GetAllByCategory("town");
            var dropdown = win.panel.AddDropdown(coats, coats.ElementAt(S.Towns().GetAll().Length % coats.Count).id,
                coat =>
                {
                    UIHelper.UpdateButtonImageColor(button, L.b.coats[coat].color);
                });
            
            button = win.panel.AddImageTextButton("Found the town", DataAction().Icon, () =>
            {
                win.Close();
                FoundTown(info, pos, townName.text, coats[dropdown.value].id);
            }, DataAction().sound);
            UIHelper.UpdateButtonImageColor(button, L.b.coats[coats[dropdown.value].id].color);
            win.Finish(400);
        }

        protected void FoundTown(MapElementInfo info, NVector pos, string townName, string coat)
        {
            int tid = S.Towns().Create(townName, pos);
            S.Town(tid).coat = coat;
            OnMapUI.Get().UpdatePanel(pos);
            S.ActPlayer().UpdateButtonMenu();
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