using System.Collections.Generic;
using DataTypes;
using Help;
using UI;
using UnityEngine;

namespace Players
{
    public class ResearchWindow
    {

        public static void ShowResearchWindow()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Research window",null);

            
            b.AddElement(new ResearchStatusSplitElement());
            b.AddElement(new ResearchFinishSplitElement());
            b.AddElement(new HelpSplitElement("research"));
            b.Finish();
        }
        
        private class ResearchStatusSplitElement : WindowBuilderSplit.SplitElement
        {
        
            public ResearchStatusSplitElement() : base("Actual", SpriteHelper.LoadIcon("magic:research"))
            {
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                ResearchMgmt mgmt = PlayerMgmt.ActPlayer().research;

                if (mgmt.status != null)
                {
                    panel.AddLabel("Status: " + mgmt.status);
                }
                
                panel.AddHeaderLabel("Actual");
                if (mgmt.actual == null || mgmt.actual.Count == 0)
                {
                    panel.AddImageLabel("No Research at the moment", "ui:no");
                }
                else
                {
                    foreach (string e in mgmt.actual)
                    {
                        Element el = Data.element[e];
                        panel.AddImageLabel(el.name, el.icon);
                    }
                    
                }
                
                List<string> ele = new List<string>();
                
                panel.AddHeaderLabel("New Research area");
                foreach (Element e in Data.element)
                {
                    panel.AddCheckbox(false, e.name, b =>
                    {
                        if (b)
                        {
                            ele.Add(e.id);
                        }
                        else
                        {
                            ele.Remove(e.id);
                        }
                    });
                }

                panel.AddButton("Begin new research", () => { mgmt.BeginNewResearch(ele); window.CloseWindow(); });


            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
        
        
        private class ResearchFinishSplitElement : WindowBuilderSplit.SplitElement
        {
        
            public ResearchFinishSplitElement() : base("Finish", SpriteHelper.LoadIcon("ui:yes"))
            {
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                panel.AddHeaderLabel("Finish researches");
                foreach (Research r in Data.research)
                {
                    if (PlayerMgmt.ActPlayer().research.Finish(r.id))
                        panel.AddImageLabel(r.name, r.GetIcon());
                }

                if (panel.Count() == 1)
                {
                    panel.AddImageLabel("No finish research.", "ui:no");
                }
            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
    }
}