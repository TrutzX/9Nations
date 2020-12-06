using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries;
using Libraries.Elements;
using UI;
using UI.Show;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Players.PlayerResearches
{
    public class ResearchStatusSplitElement : SplitElement
    {
        private Text desc;
        private Button start;
        private List<string> elements;
        
        public ResearchStatusSplitElement() : base("Actual", SpriteHelper.Load("research"))
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            PlayerResearchMgmt mgmt = S.ActPlayer().research;
                
            panel.AddHeaderLabel("Actual");
            if (mgmt.actual == null || mgmt.actual.Count == 0)
            {
                panel.AddImageLabel("No Research at the moment", "no");
            }
            else
            {
                foreach (string e in mgmt.actual)
                {
                    Element el = L.b.elements[e];
                    panel.AddImageLabel(el.Name(), el.Icon);
                }
                    
            }
                
            elements = new List<string>();
                
            panel.AddHeaderLabel("New Research area");
            foreach (string en in S.ActPlayer().elements.elements)
            {
                Element e = L.b.elements[en];
                panel.AddImageTextButton($"Add {e.Name()}", e.Sprite(), (() =>
                {
                    elements.Add(e.id);
                    UpdateDesc();
                }));
            }

            panel.AddHeaderLabel("Control");
            desc = panel.AddLabel("???? ???? ???? ???? ???? ???? ???? ???? ???? ???? ???? ???? ");
            panel.AddButton("Clear actual plan", () => { elements.Clear(); UpdateDesc(); });
            start = panel.AddButton("??", () => { mgmt.BeginNewResearch(elements); window.CloseWindow(); });
            UpdateDesc();

            if (S.Debug())
            {
                panel.AddSubLabel($"Act cost",S.ActPlayer().research.cost.ToString());
                panel.AddHeaderLabel("General possible");
                panel.AddLabel(String.Join(",",S.ActPlayer().research.AvailableResearch().Select(r => r.Name())));
                if (mgmt.actual != null)
                {
                    panel.AddHeaderLabel("Actual possible");
                    panel.AddLabel(String.Join(",",S.ActPlayer().research.AvailableResearch(mgmt.actual).Select(r => r.Name())));
                }
            }
        }

        private void UpdateDesc()
        {
            if (elements.Count == 0)
            {
                desc.text = "New research: No element selected";
                start.enabled = false;
                UIHelper.UpdateButtonText(start, "Select at least one element first to start your research");
                return;
            }

            desc.text = "New research: "+String.Join(", ", elements);
            start.enabled = true;
            UIHelper.UpdateButtonText(start, "Begin new research");
        }
        
        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}