using System;
using System.Collections.Generic;
using Actions;
using DataTypes;
using InputAction;
using Players;
using reqs;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public abstract class MapElementUI<T> : MonoBehaviour where T : MapElementInfo
    {
        public GameObject info;
        public GameObject actions;
        public Text infotext;
        protected ActiveAction ActiveAction;
        public T active;
        
        
        public void SetPanelMessage(string text)
        {
            infotext.color = Color.white;
            infotext.text = text;
        }
        
        public void SetPanelMessageError(string text)
        {
            infotext.color = Color.magenta;
            infotext.text = text;
            NAudio.PlayBuzzer();
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
            if (!ReqHelper.CheckOnlyFinal(PlayerMgmt.ActPlayer(), ActionHelper.GenReq(act), building,
                building.X(), building.Y()))
            {
                return;
            }

            GameObject button = UIElements.CreateImageButton(act.icon, actionPanel.transform, () =>
            {
                string mess = NLib.GetAction(act.id).ButtonRun(building, building.X(), building.Y(), sett);
                if (mess != null)
                {
                    SetPanelMessageError(mess);
                }
            }, act.sound);
            
            
            UIHelper.HoverEnter(infotext,$"{InputKeyHelper.ActionName(act)}, Cost:{act.cost}/{building.data.ap} AP",button,
                () => { SetPanelMessage(building.Status(PlayerMgmt.ActPlayerID())); });
        }

        protected void UpdateInfoButton()
        {
            //show unit
            gameObject.SetActive(true);
        
            //show icon
            info.GetComponentsInChildren<Image>()[1].sprite = active.GetComponent<SpriteRenderer>().sprite;
            SetPanelMessage(active.Status(PlayerMgmt.ActPlayerID()));
        
            info.GetComponent<Button>().onClick.RemoveAllListeners();
            info.GetComponent<Button>().onClick.AddListener(() =>
            {
                active.ShowInfoWindow();
            });
        }

        public abstract void UpdatePanel(T building);
        
        protected void AddButtons()
        {
            //remove actions
            UIHelper.ClearChild(actions);

            //has active action?
            if (ActiveAction != null)
            {
                SetPanelMessage(ActiveAction.PanelMessage());
                NAction action = Data.nAction[ActiveAction.id];
                UIHelper.CreateImageTextButton("Cancel " + action.name, SpriteHelper.LoadIcon(action.icon), actions.transform,
                    () => { OnMapUI.Get().SetActiveAction(null, active.GetComponent<BuildingInfo>() != null); }, "cancel");
                return;
            }

            AddAllActionButtons();
            
        }
        
        public void SetActiveAction(ActiveAction activeAction)
        {
            this.ActiveAction = activeAction;
            UpdatePanel(active);
        }

        public abstract void AddAllActionButtons();
    }
}