using System;
using Actions;
using DataTypes;
using Players;
using reqs;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class MapElementUI : MonoBehaviour
    {
        public GameObject actions;
        public Text infotext;
        
        public void SetPanelMessage(string text)
        {
            infotext.text = text;
        }
        
        public void AddActionButton(string action, string sett, MapElementInfo building, GameObject actionPanel)
        {
        
            NAction act = Data.nAction[action];

            if (act == null)
            {
                throw new MissingMemberException($"Action {action} with setting {sett} is missing.");
            }
        
            //can add under construction?
            if (building.IsUnderConstrution() && !act.useUnderConstruction)
            {
                return;
            }
        
            //can add from diff player?
            if (act.onlyOwner && !building.Owner(PlayerMgmt.ActPlayerID()))
            {
                return;
            }
        
            //can add final?
            if (!ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), ActionHelper.GenReq(act), building.gameObject,
                building.X(), building.Y()))
            {
                return;
            }
        
            GameObject button = UIElements.CreateImageButton(act.icon, actionPanel.transform, () =>
            {
                NLib.GetAction(act.id).ButtonRun(building, building.X(), building.Y(), sett, this);
            }, act.sound);
            
            
            UIHelper.HoverEnter(infotext,$"{act.name}, Cost:{act.cost}/{building.data.ap} AP",button,
                () => { SetPanelMessage(building.Status(PlayerMgmt.ActPlayerID())); });
        }
    }
}