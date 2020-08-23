using System;
using Audio;
using Buildings;

using Game;
using Libraries;
using Libraries.Rounds;
using Towns;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Players.Infos
{
    public class InfoUI : MonoBehaviour, IMapUI
    {
        public GameObject infoButtons;
        public Text infoText;

        public void UpdatePanel()
        {
            //remove actions
            UIHelper.ClearChild(infoButtons);

            bool found = false;
            
            //add new
            foreach (Info info in S.ActPlayer().info.infos)
            {
                if (info.round != GameMgmt.Get().gameRound.Round)
                {
                    break;
                }
                
                if (info.read) continue;

                AddInfoButton(info);
                found = true;
            }
            
            //
            if (!found)
            {
                ShowTown();
            }
        }

        public void ShowTown()
        {

            
            //remove actions
            UIHelper.ClearChild(infoButtons);
            
            //has element?
            MapElementInfo mei = OnMapUI.Get().GetActive();

            if (mei == null)
            {
                return;
            }
            
            Debug.Log("show town");
            Debug.Log(mei);
            
            //has town?
            Town town = mei.Town();
            if (town == null)
            {
                return;
            }
            
            //open town
            var button = UIElements.CreateImageButton(town.GetIcon(), infoButtons.transform, town.ShowDetails);
            button.transform.GetChild(0).GetComponent<Image>().color = town.Coat().color;
            UIHelper.HoverEnter(button,() => ShowPanelMessage(S.T("townDetails",mei.name,town.name)),() => ShowPanelMessage(""));

            //todo limit res int max = 10;
            // show res
            foreach (var r in L.b.res.Values())
            {
                if (!town.KnowRes(r.id) || r.special)
                {
                    continue;
                }

                int count = string.IsNullOrEmpty(r.combine) ? town.GetRes(r.id) : town.GetCombineRes(r.id);
                
                var img = UIElements.CreateImageCounter(infoButtons.transform, count, r.Icon);
                UIHelper.HoverEnter(img,() => ShowPanelMessage(S.T("townRes",town.name, r.Text(count))),() => ShowPanelMessage(""));
            }
        }
        
        public void AddInfoButton(Info info)
        {
            Button button = UIElements.CreateImageButton(SpriteHelper.Load(info.icon), infoButtons.transform, null);

            Action del = () =>
            {
                NAudio.PlayCancel();
                infoText.text = "";
                info.read = true;
                Destroy(button.gameObject);
                
                //show res?
                if (infoButtons.GetComponentsInChildren<Transform>().Length <= 3)
                {
                    UpdatePanel();
                }
            };

            if (info.action == null)
            {
                button.onClick.AddListener(() =>
                {
                    del();
                    //todo add desc?
                    UIHelper.ShowOk("Notification",info.title);
                });
            }
            else
            {
                button.onClick.AddListener(info.CallAction);
            }
            
            
            button.gameObject.AddComponent<ClickableObject>();
            button.GetComponent<ClickableObject>().right = del;
            
            UIHelper.HoverEnter(button,() => ShowPanelMessage(info.title),() => ShowPanelMessage(""));
            
            //show?
            if (info.desc != null)
            {
                info.ShowImportant();
            }
        }

        public void ShowPanelMessage(string text)
        {
            infoText.color = Color.white;
            infoText.text = text;
        }

        public void ShowPanelMessageError(string text)
        {
            infoText.color = Color.magenta;
            infoText.text = text;
            NAudio.PlayBuzzer();
        }
    }
}