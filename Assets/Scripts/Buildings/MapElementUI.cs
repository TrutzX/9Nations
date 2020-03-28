using System;
using System.Collections.Generic;
using Actions;
using Classes.Actions;
using DataTypes;
using Game;
using InputAction;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using reqs;
using Tools;
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
        
        
        public void ShowPanelMessage(string text)
        {
            infotext.color = Color.white;
            infotext.text = text;
        }
        
        public void ShowPanelMessageError(string text)
        {
            if (text == null)
            {
                ShowPanelMessage("");
                return;
            }
            
            infotext.color = Color.magenta;
            infotext.text = text;
            NAudio.PlayBuzzer();
        }
        
        public void AddNewActionButton(ActionHolders holder, ActionHolder action, MapElementInfo info, GameObject actionPanel)
        {

            if (action == null)
            {
                throw new MissingMemberException($"Action for {info} is missing.");
            }

            BasePerformAction ba = action.PerformAction();
            FDataAction da = action.DataAction();
        
            //can add under construction?
            if (info.IsUnderConstruction() && !da.useUnderConstruction)
            {
                return;
            }
        
            //can add from diff player?
            if (da.onlyOwner && !info.Owner(PlayerMgmt.ActPlayerID()))
            {
                return;
            }
        
            //can add final?
            if (!action.req.Check(PlayerMgmt.ActPlayer(), info, info.Pos(), true))
            {
                return;
            }

            GameObject button = UIElements.CreateImageButton(da.Sprite(), actionPanel.transform, () =>
            {
                //can use?
                if (!action.req.Check(info.Player(), info, info.Pos()))
                {
                    ShowPanelMessageError(action.req.Desc(info.Player(), info, info.Pos()));
                    return;
                }
                
                //check ap
                if (da.cost > info.data.ap)
                {
                    int round = 1 + (da.cost - info.data.ap) / info.data.apMax;
                
                    WindowPanelBuilder wpb = WindowPanelBuilder.Create("Do you want to wait?");
                    wpb.panel.AddImageLabel($"Action {da.name} need {da.cost - info.data.ap} AP more. You can wait {round} rounds.", "round");
                    wpb.panel.AddButton($"Wait {round} rounds",() =>
                    {
                        info.SetWaitingAction(holder.actions.IndexOf(action));
                        OnMapUI.Get().UpdatePanel(info.Pos());
                        wpb.Close();
                    });
                    wpb.AddClose();
                    wpb.Finish();
                    return;
                }

                string mess = holder.Perform(action, ActionEvent.Direct, PlayerMgmt.ActPlayer(), info, info.Pos());
                if (mess != null)
                {
                    ShowPanelMessageError(mess);
                }
            }, da.sound);
            
            UIHelper.HoverEnter(infotext,$"{InputKeyHelper.ActionName(da)}, Cost:{da.cost}/{info.data.ap} AP",button,
                () => { ShowPanelMessage(info.Status(PlayerMgmt.ActPlayerID())); });
        }

        protected void UpdateInfoButton()
        {
            //show unit
            gameObject.SetActive(true);
        
            //show icon
            info.GetComponentsInChildren<Image>()[1].sprite = active.GetComponent<SpriteRenderer>().sprite;
            ShowPanelMessage(active.Status(PlayerMgmt.ActPlayerID()));
        
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
                ShowPanelMessage(ActiveAction.PanelMessage());
                NAction action = Data.nAction[ActiveAction.id];
                UIHelper.CreateImageTextButton("Cancel " + action.name, SpriteHelper.Load(action.icon), actions.transform,
                    () => { OnMapUI.Get().SetActiveAction(null, active.GetComponent<BuildingInfo>() != null); }, "cancel");
                return;
            }

            //has waiting action?
            if (active.data.actionWaitingPos != -1)
            {
                ActionHolder a = active.data.action.actions[active.data.actionWaitingPos];
                UIHelper.CreateImageTextButton($"Cancel preparation", a.DataAction().Sprite(), actions.transform,
                    () =>
                    {
                        active.data.ap = Math.Min(active.data.ActionWaitingAp, active.data.apMax); 
                        active.SetWaitingAction(-1); 
                        OnMapUI.Get().UpdatePanel(active.Pos()); 
                    }, "cancel");
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