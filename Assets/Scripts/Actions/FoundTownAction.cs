using System;
using System.ComponentModel;
using Buildings;
using Players;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class FoundTownAction : BaseAction
    {
        protected override void ButtonAction(Player player, MapElementInfo gameObject, int x, int y, string settings)
        {
            //create found window
            WindowPanelBuilder win = WindowPanelBuilder.Create("Found a new town");
            GameObject name = win.panel.AddInput("town name",NGenTown.GetTownName(PlayerMgmt.ActPlayer().Nation().TownNameGenerator),s => {});
            win.panel.AddButton("Generate new name", () =>
            {
                name.GetComponent<InputField>().text = NGenTown.GetTownName(PlayerMgmt.ActPlayer().Nation().TownNameGenerator);
                //name.GetComponentsInChildren<Text>()[0].text = NGenTown.GetTownName(PlayerMgmt.Get().GetActPlayer().nation);
                //name.GetComponentsInChildren<Text>()[1].text = NGenTown.GetTownName(PlayerMgmt.Get().GetActPlayer().nation);
            });
            win.panel.AddButton("Found the town", () => { win.Close();FoundTown(gameObject, x, y, name.GetComponent<InputField>().text); });
            win.Finish(400);
        }

        protected virtual void FoundTown(MapElementInfo gameObject,int x, int y, string townName)
        {
            TownMgmt.Get().Create(townName, x, y);
            OnMapUI.Get().UpdatePanelXY(x, y);
            PlayerMgmt.ActPlayer().UpdateButtonTop();
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
    }
    
    public class FoundTownKillAction : FoundTownAction
    {

        protected override void FoundTown(MapElementInfo gameObject,int x, int y, string townName)
        {
            gameObject.Kill();
        }
    }
}