using System;
using System.Collections.Generic;

using Audio;
using Classes.Actions;
using Classes.Actions.Addons;

using Game;
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
    public abstract class MapElementUI<T> : MonoBehaviour, IMapUI where T : MapElementInfo
    {
        public GameObject info;
        public GameObject actions;
        public Text infoText;
        protected BaseActiveAction ActiveAction;
        public T active;


        public void ShowPanelMessage(string text)
        {
            infoText.color = Color.white;
            infoText.text = text;
        }

        public void ShowPanelMessageError(string text)
        {
            if (text == null)
            {
                ShowPanelMessage("");
                return;
            }

            infoText.color = Color.magenta;
            infoText.text = text;
            NAudio.PlayBuzzer();
        }

        public void AddNewActionButton(ActionHolders holder, ActionHolder action, MapElementInfo info,
            GameObject actionPanel)
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
            if (da.onlyOwner && !info.Owner(S.ActPlayerID()))
            {
                return;
            }

            //can add final?
            if (!action.req.Check(S.ActPlayer(), info, info.Pos(), true))
            {
                return;
            }
            
            Button button = UIElements.CreateImageButton(da.Sprite(), actionPanel.transform, () =>
            {
                PerformAction(holder, action, info);
            }, da.sound);

            UIHelper.HoverEnter(button,
                () => { ShowPanelMessage($"{LSys.tem.inputs.ActionName(da)}, Cost:{da.cost}/{info.data.ap} AP"); },
                () => { ShowPanelMessage(info.Status(S.ActPlayerID())); });
        }

        private void PerformAction(ActionHolders holder, ActionHolder action, MapElementInfo info)
        {
            FDataAction da = action.DataAction();
            //can use?
            if (!action.req.Check(info.Player(), info, info.Pos()))
            {
                //TODO not hardcoded
                //start interact action?
                if (da.mapElement && holder.Contains("interact") && da.field == "near")
                {
                    PerformAction(holder, holder.Get("interact") ,info);
                    return;
                }

                ShowPanelMessageError(action.req.Desc(info.Player(), info, info.Pos()));
                return;
            }

            //check ap
            if (da.cost > info.data.ap)
            {
                ActionHelper.WaitRound(holder, action, info, info.Pos());
                return;
            }

            string mess = holder.Perform(action, ActionEvent.Direct, S.ActPlayer(), info, info.Pos());
            if (mess != null)
            {
                ShowPanelMessageError(mess);
            }
        }

        protected void UpdateInfoButton()
        {
            //show unit
            gameObject.SetActive(true);

            //show icon
            info.GetComponentsInChildren<Image>()[1].sprite = active.Sprite();
            ShowPanelMessage(active.Status(S.ActPlayerID()));

            info.GetComponent<Button>().onClick.RemoveAllListeners();
            info.GetComponent<Button>().onClick.AddListener(() => { active.ShowInfoWindow(); });
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
                FDataAction action = ActiveAction.DataAction();
                UIHelper.CreateImageTextButton("Cancel " + action.Name(), action.Sprite(), actions.transform,
                    () => { OnMapUI.Get().SetActiveAction(null, active.GetComponent<BuildingInfo>() != null); },
                    "cancel");
                return;
            }

            //has waiting action?
            if (active.data.actionWaitingActionPos != -1)
            {
                ActionHolder a = active.data.action.actions[active.data.actionWaitingActionPos];
                UIHelper.CreateImageTextButton($"Cancel preparation", a.DataAction().Sprite(), actions.transform,
                    () =>
                    {
                        active.data.ap = Math.Max(0,Math.Min(active.data.actionWaitingAp, active.data.apMax));
                        active.SetWaitingAction(-1, null);
                        OnMapUI.Get().UpdatePanel(active.Pos());
                    }, "cancel");
                return;
            }

            AddAllActionButtons();
        }

        public void SetActiveAction(BaseActiveAction activeAction)
        {
            this.ActiveAction = activeAction;
            UpdatePanel(active);
        }

        public abstract void AddAllActionButtons();
    }
}