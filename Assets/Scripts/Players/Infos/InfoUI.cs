using System;
using Audio;
using Buildings;

using Game;
using Libraries.Rounds;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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
            
            //add new
            foreach (Info info in PlayerMgmt.ActPlayer().info.infos)
            {
                if (info.round != GameMgmt.Get().gameRound.Round)
                {
                    break;
                }

                AddInfoButton(info);
            }
        }
        
        public void AddInfoButton(Info info)
        {
            Button button = UIElements.CreateImageButton(SpriteHelper.Load(info.icon), infoButtons.transform, null);

            Action del = () =>
            {
                NAudio.PlayCancel();
                infoText.text = "";
                Destroy(button);
            };

            if (info.action == null)
            {
                button.onClick.AddListener(() => del());
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