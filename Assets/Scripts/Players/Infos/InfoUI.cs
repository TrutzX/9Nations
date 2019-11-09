using System;
using DataTypes;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Players.Infos
{
    public class InfoUI : MonoBehaviour
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
                if (info.round != RoundMgmt.Get().Round)
                {
                    break;
                }

                AddInfoButton(info);
            }
        }
        
        public void AddInfoButton(Info info)
        {
            GameObject button = UIElements.CreateImageButton(SpriteHelper.Load(info.icon), infoButtons.transform, null);

            Action del = () =>
            {
                NAudio.PlayCancel();
                infoText.text = "";
                Destroy(button);
            };

            if (info.action == null)
            {
                button.GetComponent<Button>().onClick.AddListener(() => del());
            }
            else
            {
                button.GetComponent<Button>().onClick.AddListener(info.CallAction);
            }
            
            
            button.AddComponent<ClickableObject>();
            button.GetComponent<ClickableObject>().right = del;
            
            UIHelper.HoverEnter(infoText,info.title,button,() => { infoText.text = ""; });
            
            //show?
            if (info.desc != null)
            {
                info.ShowImportant();
            }
        }
    }
}