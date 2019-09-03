using System;
using System.ComponentModel;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class FoundTownAction : BaseAction
    {
        protected override void ButtonAction(Player player, GameObject gameObject, int x, int y, string settings)
        {
            //create found window
            WindowPanelBuilder win = WindowPanelBuilder.Create("Found a new town");
            GameObject name = win.panel.AddInput("town name",NGenTown.GetTownName(PlayerMgmt.ActPlayer().nation),s => {});
            win.panel.AddButton("Generate new name", () =>
            {
                name.GetComponent<InputField>().text = NGenTown.GetTownName(PlayerMgmt.ActPlayer().nation);
                //name.GetComponentsInChildren<Text>()[0].text = NGenTown.GetTownName(PlayerMgmt.Get().GetActPlayer().nation);
                //name.GetComponentsInChildren<Text>()[1].text = NGenTown.GetTownName(PlayerMgmt.Get().GetActPlayer().nation);
            });
            win.panel.AddButton("Found the town", () =>
            {
                Debug.Log("found");
                TownMgmt.Get().Create(name.GetComponent<InputField>().text,  x,  y);
                Destroy(win.gameObject);
                OnMapUI.Get().UpdatePanelXY(x,y);
            });
            win.Finish(400);
        }

        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }
    }
}